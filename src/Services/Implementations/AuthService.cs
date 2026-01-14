using E_Commerce.Mappers;
using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Request.Auth;
using E_Commerce.Dtos;
using E_Commerce.Results;
using E_Commerce.Services.Abstractions;
using E_Commerce.Models.Auth;
using E_Commerce.Services.Identity;
using E_Commerce.Requests.Auth;
using E_Commerce.Results.Errors;

namespace E_Commerce.Services.Implementations;

public class AuthService(IUserRepository userRepo,
                         ICustomerRepository customerRepo,
                         IVendorRepository vendorRepo,
                         IAdminRepository adminRepo,
                         JwtTokenProvider tokenProvider,
                         IUnitOfWork uow) 
    : IAuthService
{
    public async Task<Result<UserDto>> RegisterCustomerAsync(RegisterCustomerRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var userResult = await RegisterUserAsync(request, UserRole.Customer, ct);

        if (userResult.IsFailure)
        {
            return userResult.Errors;
        }

        User user = userResult.Value!;

        await userRepo.AddAsync(user, ct);

        var customerResult = Customer.Create
        (
            userId: user.Id,
            firstName: request.FirstName!,
            lastName: request.LastName!,
            shippingAddress: request.ShippingAddress,
            billingAddress: request.BillingAddress
        );

        if (customerResult.IsFailure)
        {
            return customerResult.Errors;
        }

        Customer customer = customerResult.Value!;

        await customerRepo.AddAsync(customer, ct);

        await uow.SaveChangesAsync(ct);

        return customer.ToDto();
    }

    public async Task<Result<UserDto>> RegisterVendorAsync(RegisterVendorRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var userResult = await RegisterUserAsync(request, UserRole.Vendor, ct);

        if (userResult.IsFailure)
        {
            return userResult.Errors;
        }

        User user = userResult.Value!;

        await userRepo.AddAsync(user, ct);

        var vendorResult = Vendor.Create
        (
            userId: user.Id,
            storeName: request.StoreName
        );

        if (vendorResult.IsFailure)
        {
            return vendorResult.Errors;
        }

        Vendor vendor = vendorResult.Value!;

        await vendorRepo.AddAsync(vendor, ct);

        await uow.SaveChangesAsync(ct);

        return vendor.ToDto();
    }
    
    public async Task<Result<UserDto>> RegisterAdminAsync(RegisterAdminRequest request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var userResult = await RegisterUserAsync(request, UserRole.Admin, ct);

        if (userResult.IsFailure)
        {
            return userResult.Errors;
        }

        User user = userResult.Value!;

        await userRepo.AddAsync(user, ct);

        var adminResult = Admin.Create
        (
            userId: user.Id
        );

        if (adminResult.IsFailure)
        {
            return adminResult.Errors;
        }

        Admin admin = adminResult.Value!;

        await adminRepo.AddAsync(admin, ct);

        await uow.SaveChangesAsync(ct);

        return admin.ToDto();
    }

    private async Task<Result<User>> RegisterUserAsync(RegisterUserRequest request, UserRole role, CancellationToken ct)
    {
        if (await userRepo.IsEmailExist(request.Email, ct))
            return UserErrors.EmailInUse(request.Email!);

        if (request.PhoneNumber is not null && await userRepo.IsPhoneNumberExist(request.PhoneNumber, ct))
            return UserErrors.PhoneNumberInUse(request.PhoneNumber);

        var userResult = User.Create
        (
            email: request.Email!,
            phoneNumber: request.PhoneNumber,
            passwordHash: request.Password!, // TODO: add password hashing
            role: role
        );

        return userResult.IsFailure ? userResult.Errors : userResult.Value!;
    }
    
    public async Task<Result<TokenDto>> LogInAsync(LogInUserRequest request, CancellationToken ct)
    {
        User? user = await userRepo.GetByEmailAsTrackingAsync(request.Email!, ct);

        if (user is null)
            return LoginErrors.LoginInvalidCredentials;
        
        if (user.PasswordHash != request.Password) // TODO: replace with password hash verify
            return LoginErrors.LoginInvalidCredentials;

        if (user.IsActive == false)
            return LoginErrors.LoginAccountInActive;

        user.UpdateLastLogin();

        var generateTokenRequest = new GenerateTokenRequest
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role,
        };

        var tokeDtoResult = await tokenProvider.CreateAsync(generateTokenRequest, ct);

        if (tokeDtoResult.IsFailure)
        {
            return tokeDtoResult.Errors;
        }

        await uow.SaveChangesAsync(ct);

        return tokeDtoResult.Value!;
    }

    public Task<Result> LogOutAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<TokenDto>> Refresh(RefreshTokenRequest request, CancellationToken ct)
    {
        var tokeDtoResult = await tokenProvider.RefreshAsync(request, ct);

        if (tokeDtoResult.IsFailure)
        {
            return tokeDtoResult.Errors;
        }

        await uow.SaveChangesAsync(ct);

        return tokeDtoResult.Value!;
    }
}