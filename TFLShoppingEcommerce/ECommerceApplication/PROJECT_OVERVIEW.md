# ECommerceApplication — Project Overview

## Problem statement
Traditional small-to-medium sellers and campus/demo storefronts often struggle to deliver a complete online shopping experience that includes **catalog browsing, cart, checkout, order tracking, and customer communication**. Manual order handling and missing automation (payments, OTP/password recovery, status updates, receipts) leads to errors, poor user experience, and low trust.

This project addresses that gap by providing a working end-to-end e-commerce web application with:
- product discovery (catalog/categories/search-style browsing)
- cart → order → payment flows
- email-based OTP recovery and order confirmation
- automated order-status progression via a background task

## Motivation
- Provide a **single, cohesive** web app that demonstrates core e-commerce workflows.
- Reduce operational friction using **automation** (emails, OTP, background updates).
- Use a familiar enterprise stack (**ASP.NET Core MVC + MySQL**) that’s easy to maintain and extend.

## Goals and objectives
- **Core shopping journey**: browse → add to cart → checkout → pay (online/COD) → confirmation.
- **Customer lifecycle**: register/login, manage profile and addresses, recover password via OTP.
- **Commerce features**: wishlist, coupons/discounts, product reviews.
- **Operational support**: order records, payment records, status updates (scheduled).

## Project scope
- **In-scope**:
  - Web UI (Razor views) + MVC controllers
  - MySQL-backed persistence via repositories
  - Session-based login state
  - Razorpay integration for online payments
  - SMTP email for OTP and order confirmations
  - Background status updates for orders
- **Out-of-scope / not implemented as a full product** (based on current code):
  - Full role-based auth (Admin/User) using ASP.NET Core Identity
  - Token-based APIs / mobile clients
  - Multi-tenant storefronts, inventory warehousing, shipping carrier integration
  - Distributed session storage (e.g., Redis) for multi-instance deployments

## Functional requirements (what the system does)
- **Authentication & account**:
  - Register and login users
  - Maintain login state via session
  - Logout
  - Forgot-password flow using OTP via email
- **Catalog & browsing**:
  - View products and categories (catalog pages)
  - View product details
- **Cart & checkout**:
  - Add/remove/update cart items
  - Create an order from the cart
  - Choose delivery address
- **Payments**:
  - Online payment via Razorpay checkout flow
  - Cash-on-delivery style payment recording
  - Save payment status for an order
- **Orders**:
  - Store order items and totals (including discount/final amount)
  - Background updates for order statuses
- **Engagement & promotions**:
  - Wishlist management
  - Coupon application/discount handling
  - Reviews/ratings
- **Notifications**:
  - OTP emails
  - Order confirmation emails with an invoice-like summary

## Non-functional requirements (quality attributes)
- **Security**:
  - Secrets must not be stored in source (use user-secrets/env vars)
  - Session cookies should be HTTP-only (configured)
  - Payment verification should be robust (signature verification is typically required)
- **Reliability**:
  - Background processing should be resilient to transient DB failures
  - Email sending should not crash the request pipeline
- **Performance**:
  - Common reads (catalog/cart) should be responsive
  - Avoid blocking operations where possible for scale (current implementation is synchronous)
- **Maintainability**:
  - Separation of concerns via Controller/Service/Repository layers
  - Interfaces enable unit testing/mocking

## What this project is
`ECommerceApplication` is an **ASP.NET Core MVC** e-commerce web application targeting **.NET 8**. It follows a layered structure:

- **Controllers** handle HTTP requests and return MVC views.
- **Services** contain business logic.
- **Repositories** handle database access (MySQL).
- **Views** (`.cshtml`) render UI; **wwwroot** serves static assets.

Key capabilities visible in the codebase include: product catalog, categories, shopping cart, wishlist, coupons, reviews, customer/profile/address management, order processing, payment processing (Razorpay), OTP-based password reset, email notifications, and background order-status updates.

## Tech stack
- **Runtime / framework**: .NET 8 (`net8.0`), ASP.NET Core MVC
- **Database**: MySQL (`MySql.Data`)
- **Payments**: Razorpay (`Razorpay`)
- **Email**: SMTP via MailKit (`MailKit`)
- **UI**: Razor Views + static assets in `wwwroot` (Bootstrap/jQuery present)
- **State**: ASP.NET **Session** (in-memory distributed cache)
- **Background jobs**: `BackgroundService` for periodic order status updates

NuGet dependencies are defined in `ECommerceApplication.csproj`.

## Solution / entry point
- **Solution**: `ECommerceApplication.sln`
- **Project**: `ECommerceApplication.csproj`
- **Startup**: `Program.cs`

In `Program.cs`, MVC is enabled and the app wires up DI for repository/service pairs, configures **Session**, serves **static files**, and maps the default MVC route:

- Default route pattern: `{controller=Home}/{action=Index}/{id?}`

## Folder structure (high-level)
- **`Controllers/`**: MVC controllers (web endpoints)
- **`Models/`**: Domain/data models used by controllers/services/views
- **`Repository/`**: Data access layer + repository interfaces
- **`Services/`**: Business logic layer + service interfaces + hosted background service
- **`Util/`**: Utility helpers (notably DB connection)
- **`Views/`**: Razor views grouped by controller
- **`wwwroot/`**: Static files (css/js/images/libs)
- **`Properties/`**: Project properties / launch settings (if present)

Build artifacts:
- **`bin/`**, **`obj/`**: build outputs (typically not part of source documentation)

## Application layers and request flow
Typical flow for a web request:

1. **Browser → Controller**
2. **Controller → Service** (business rules, orchestration)
3. **Service → Repository** (database read/write)
4. **Repository → MySQL** (via `MySql.Data`)
5. **Controller → View** (`.cshtml`) or JSON response

## Methodology / approach
This codebase follows a practical implementation methodology aligned with typical MVC system design:

- **Requirements → modules**: identify primary e-commerce modules (catalog, cart, orders, payments, user account).
- **Layered architecture**: keep HTTP concerns in controllers, business rules in services, and DB interaction in repositories.
- **Incremental feature build-out**: add flows end-to-end (e.g., cart → order → payment → confirmation email).
- **Integration-driven development**:
  - integrate payment gateway (Razorpay) at checkout
  - integrate SMTP email for OTP and receipts
  - add a hosted background service for periodic order-status updates

## Controllers (what they likely own)
Controllers present in `Controllers/`:

- **`HomeController`**: landing pages
- **`AuthenticationController`**: login/register/logout + forgot-password OTP flow
- **`OtpController`**: OTP endpoints (paired with `OtpService`)
- **`CatelogController`**: product catalog browsing (spelling as in code)
- **`ProductController`**, **`CategoryController`**: product/category pages & actions
- **`ShoppingCartController`**: cart operations
- **`CustomerController`**, **`ProfileController`**, **`CustomerAddressController`**: customer profile & address book
- **`OrderProcessingController`**: placing/managing orders
- **`PaymentProcessingController`**: checkout + payment verification + COD payment recording
- **`WishlistController`**: wishlist operations
- **`CouponController`**: coupon application/management
- **`ReviewController`**: reviews/ratings

## Services (business logic)
Services are defined under `Services/` with interfaces in `Services/interfaces/`.

Notable services:
- **`ProductServices`**, **`CategoryService`**: catalog logic
- **`ShoppingCartService`**: cart calculations and cart operations
- **`CustomerService`** and **`CustomerAddreessService`**: customer/address logic (note spelling in filename)
- **`OrderProcessingService`**: order creation, totals, and order items
- **`PaymentProcessingService`**: payment recording logic
- **`ReviewService`**, **`WishlistService`**, **`CouponService`**
- **`OtpService`**: OTP generation/verification
- **`EmailService`**: sends OTP and order confirmation emails (MailKit)
- **`OrderStatusBackgroundService`**: periodic job that updates order statuses

### Background processing
`OrderStatusBackgroundService` runs every ~3 minutes and calls repository methods to update:
- **Pending orders**
- **Processing orders**
- **Shipped orders**

This service uses `IServiceScopeFactory` to resolve a scoped repository per iteration.

## Repositories (data access)
Repositories exist under `Repository/` with interfaces under `Repository/Interfaces/`.

Typical pattern:
- `IThingRepository` interface
- `ThingRepository` implementation
- `IThingService` service interface
- `ThingService` implementation

Repositories likely use `Util/DatabaseConnection.cs` to obtain a MySQL connection.

## Database configuration
### Current state in code
The MySQL connection string is currently hardcoded in:
- `Util/DatabaseConnection.cs` (`ECommerceApplication.Utils.DatabaseConnection.getConnection()`)

It uses:
- host `localhost`
- port `3306`
- user `root`
- password `password`
- database `TFLShoppingEcommerce`

### Recommendation (for production readiness)
Move connection settings to `appsettings.json`/environment variables (and keep secrets out of source), then read via `IConfiguration`.

## Session usage
Session is enabled in `Program.cs` using in-memory distributed cache, with:
- **Idle timeout**: 15 minutes
- **HTTP-only cookies**

Session keys observed in controllers:
- `Email` (logged-in user identity)
- `OrderId`, `Amount` (checkout/payment flow)
- `ResetPasswordEmail`, `ResetPasswordVerified` (forgot-password OTP flow)

Because session storage is **in-memory**, sessions will reset on app restart and won’t be shared across multiple servers without swapping to a distributed store (e.g., Redis/SQL).

## Payments (Razorpay)
The Razorpay flow is implemented in `PaymentProcessingController`:
- **Create order**: builds a Razorpay order with amount (in paise), currency INR, receipt id
- **Verify payment**: receives a JSON body with Razorpay identifiers, records payment status, and redirects

Configuration keys expected (read via `IConfiguration`):
- `Razorpay:Key`
- `Razorpay:Secret`

UI integration is in `Views/PaymentProcessing/Checkout.cshtml` and includes Razorpay checkout script.

## Email (MailKit SMTP)
Email sending is handled in `Services/EmailService.cs`:
- **OTP email** for verification
- **Order confirmation** email with an HTML invoice table

Configuration keys expected:
- `EmailService:email`
- `EmailService:password`
- `EmailService:host`
- `EmailService:port`

## Configuration files
- `appsettings.json` / `appsettings.Development.json`: currently only logging + `AllowedHosts`.
- Secrets are implied by `UserSecretsId` in `ECommerceApplication.csproj` (good for local dev).

Given code reads Razorpay and Email settings from configuration, those values must be provided via:
- user-secrets (recommended for local dev), or
- environment variables, or
- appsettings (not recommended for secrets)

## How to run (developer quickstart)
### Prereqs
- .NET SDK **8.x**
- MySQL running (and a DB matching the configured name/schema)

### Run
From the repo root:

```powershell
dotnet restore
dotnet run
```

Or open `ECommerceApplication.sln` in Visual Studio and run the project.

## Results / outputs (what you get after running)
When configured (MySQL + email + Razorpay keys), running the application provides:
- **Working web storefront UI** (Razor views) for browsing products/categories.
- **End-to-end checkout** that creates an order, records payment (online or COD), and redirects back to shopping.
- **Email-based flows**:
  - OTP emails for password recovery
  - order confirmation email with itemized invoice summary
- **Automated operations**: a background worker updates order statuses periodically.

## Why these choices (rationale)
- **ASP.NET Core MVC**: strong server-rendered web framework with clean routing, DI, and a mature ecosystem.
- **Repository + Service pattern**: keeps DB code isolated and business logic reusable/testable.
- **MySQL**: widely used relational DB, easy to run locally, fits transactional order/payment data.
- **Razor Views**: quick to build server-rendered UI without a separate SPA frontend.
- **Razorpay**: popular gateway for INR payments; provides a hosted checkout experience.
- **MailKit (SMTP)**: robust email client library for transactional emails (OTP and receipts).
- **Session (in-memory)**: simple way to store logged-in user context for a single-instance dev setup.
- **BackgroundService**: built-in mechanism for periodic tasks (order status updates) inside ASP.NET Core.

## Common functional flows (mapped to code)
- **Authentication**: `AuthenticationController` uses session key `Email` after login.
- **Forgot password**: OTP generation/verification via `OtpService` + `EmailService`.
- **Catalog browsing**: `CatelogController` + `ProductController` + `CategoryController`.
- **Cart**: `ShoppingCartController` → `IShoppingCartService`/`IShoppingCartRepository`.
- **Order placement**: `OrderProcessingController`/`OrderProcessingService`.
- **Payment**:
  - Online: `PaymentProcessingController.CreateOrder()` → Razorpay → `Verify()` → `SavePayment()`
  - COD: `SaveCodPayment()`
  - Confirmation email: `EmailService.SendOrderConfirmationEmail()`
- **Order status updates**: `OrderStatusBackgroundService` periodically updates order statuses.

## Notable implementation notes / risks (for maintainers)
- **Hardcoded DB credentials**: connection string in `Util/DatabaseConnection.cs` includes password in source.
- **Hardcoded login bypass**: `AuthenticationController` contains a hardcoded email/password branch.
- **Session-backed identity**: no ASP.NET Core Identity/Cookie auth configured; authorization middleware is present but there are no explicit auth policies shown in `Program.cs`.

## Limitations (current state)
- **Configuration gaps**: `appsettings.json` currently doesn’t include DB/email/Razorpay sections; they must be supplied via user-secrets/env vars.
- **Hardcoded DB connection string**: should be moved to configuration.
- **Auth model**: session-based email flagging; no standardized Identity setup, roles, or password hashing shown.
- **Payment verification**: production Razorpay integrations typically validate signatures; ensure verification logic is complete for real deployments.
- **Scalability**: in-memory sessions and synchronous I/O will limit multi-instance scaling.

## Future enhancements
- Move DB config to `appsettings.json` + user-secrets, add environment-specific configs.
- Introduce ASP.NET Core Identity (password hashing, lockouts, roles, secure cookies).
- Add Razorpay signature verification and stronger payment reconciliation.
- Add distributed caching/session (Redis) and async DB/email operations.
- Add observability: structured logs, metrics, and centralized error handling.
- Add tests: unit tests for services and integration tests for controllers/repositories.

## Where to look for specific changes
- **Add a new page/feature**: `Controllers/` + `Views/<ControllerName>/` + relevant `Services/` and `Repository/`
- **Change DB queries**: `Repository/*.cs` and `Util/DatabaseConnection.cs`
- **Change payment**: `Controllers/PaymentProcessingController.cs` + `Views/PaymentProcessing/Checkout.cshtml`
- **Change email templates**: `Services/EmailService.cs`
- **Change background job cadence**: `Services/OrderStatusBackgroundService.cs` (`Task.Delay(...)`)

