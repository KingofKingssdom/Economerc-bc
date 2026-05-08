using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
namespace Ecommerce.Database
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(MyAppContext context)
        {
            await context.Database.MigrateAsync();


            if (await context.Products.AnyAsync()) return;

            string sqlData = @"
            SET IDENTITY_INSERT [dbo].[Brands] ON 

            INSERT [dbo].[Brands] ([Id], [BrandCode], [BrandName], [UrlImageBrand]) VALUES (1, N'IP', N'Apple', N'Images/Brands/5f7185d2-f7b5-42e5-a9b0-3b6b99468ea7.png')
            INSERT [dbo].[Brands] ([Id], [BrandCode], [BrandName], [UrlImageBrand]) VALUES (2, N'SS', N'Samsung', N'Images/Brands/1d2211fc-90d3-4da2-aa26-e35a5add3916.png')
            INSERT [dbo].[Brands] ([Id], [BrandCode], [BrandName], [UrlImageBrand]) VALUES (3, N'XM', N'Xiaomi', N'Images/Brands/ba8f2e22-fb45-455f-af25-c7816f196b3d.png')
            INSERT [dbo].[Brands] ([Id], [BrandCode], [BrandName], [UrlImageBrand]) VALUES (4, N'MB', N'Apple', N'Images/Brands/6ee12ee7-4e89-4c0f-900d-014c0fef2698.png')
            INSERT [dbo].[Brands] ([Id], [BrandCode], [BrandName], [UrlImageBrand]) VALUES (5, N'AS', N'Asus', N'Images/Brands/346558fb-1e2e-4820-9f1a-8c92a993f8bf.png')
            INSERT [dbo].[Brands] ([Id], [BrandCode], [BrandName], [UrlImageBrand]) VALUES (6, N'DL', N'Dell', N'Images/Brands/f1cbee5a-7984-4286-89fc-54f63290aacc.png')
            INSERT [dbo].[Brands] ([Id], [BrandCode], [BrandName], [UrlImageBrand]) VALUES (7, N'AP', N'Apple', N'Images/Brands/9ba07405-3497-4884-bbd1-d547675c3f66.png')
            INSERT [dbo].[Brands] ([Id], [BrandCode], [BrandName], [UrlImageBrand]) VALUES (8, N'SN', N'Sony', N'Images/Brands/ad86d505-1ac8-42a7-9e64-d26ff35ce7a5.png')
            INSERT [dbo].[Brands] ([Id], [BrandCode], [BrandName], [UrlImageBrand]) VALUES (9, N'AW', N'Apple', N'Images/Brands/ebdd5704-6ad6-4fc7-97c9-d910813db2bb.png')
            INSERT [dbo].[Brands] ([Id], [BrandCode], [BrandName], [UrlImageBrand]) VALUES (10, N'LG', N'LG', N'Images/Brands/e6475e2c-b74a-4f47-bc07-102d89d28008.png')  
            SET IDENTITY_INSERT [dbo].[Brands] OFF
            
            SET IDENTITY_INSERT [dbo].[Categories] ON 

            INSERT [dbo].[Categories] ([Id], [CategoryCode], [CategoryName]) VALUES (1, N'DT', N'Điện Thoại')
            INSERT [dbo].[Categories] ([Id], [CategoryCode], [CategoryName]) VALUES (2, N'TB', N'Tablet')
            INSERT [dbo].[Categories] ([Id], [CategoryCode], [CategoryName]) VALUES (3, N'LT', N'Laptop')
            INSERT [dbo].[Categories] ([Id], [CategoryCode], [CategoryName]) VALUES (4, N'AU', N'Âm thanh')
            INSERT [dbo].[Categories] ([Id], [CategoryCode], [CategoryName]) VALUES (5, N'WT', N'Đồng hồ')
            INSERT [dbo].[Categories] ([Id], [CategoryCode], [CategoryName]) VALUES (6, N'SC', N'Màn hình')
            INSERT [dbo].[Categories] ([Id], [CategoryCode], [CategoryName]) VALUES (7, N'TV', N'Ti vi')
            SET IDENTITY_INSERT [dbo].[Categories] OFF

            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (1, 1)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (1, 2)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (2, 2)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (4, 2)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (5, 2)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (6, 2)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (7, 2)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (1, 3)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (2, 3)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (4, 3)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (5, 3)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (7, 3)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (3, 4)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (3, 5)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (6, 5)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (3, 6)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (6, 6)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (4, 7)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (4, 8)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (5, 9)
            INSERT [dbo].[CategoryBrands] ([CategoryId], [BrandId]) VALUES (7, 10)

            SET IDENTITY_INSERT [dbo].[Products] ON 

            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (1, N'DT001AP', N'Iphone 17 Pro max | Chính hãng', N'trả góp 0% - 0đ phụ phí - 0đ trả trước - kì hạn đến 6 tháng', N'Images/Products/8c4ea17e-94d6-46eb-a15b-289bd2a509bf.png', 1, 1, 1, 1)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (2, N'DT002AP', N'Iphone 16 Pro max | Chính hãng ', N'trả góp 0% - 0đ phụ phí - 0đ trả trước - kì hạn đến 6 tháng', N'Images/Products/eb4bb842-23a7-4549-9939-43c82f5a2f38.png', 1, 1, 1, 1)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (3, N'DT003AP', N'Iphone 15 Pro max | Chính hãng ', N'trả góp 0% - 0đ phụ phí - 0đ trả trước - kì hạn đến 6 tháng', N'Images/Products/5df33226-70ea-40c6-9319-9e200938f1d1.png', 1, 1, 1, 1)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (4, N'DT004SS', N'Samsung Galaxy S26 Ultra', N'Không phí chuyển đổi khi trả góp 0% qua thẻ tín dụng kì hạn 3-6 tháng', N'Images/Products/27af6760-29ac-4ad5-a6f6-1146208b5b00.png', 1, 1, 2, 1)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (5, N'DT005SS', N'Samsung Galaxy S25 Ultra', N'Không phí chuyển đổi khi trả góp 0% qua thẻ tín dụng kì hạn 3-6 tháng', N'Images/Products/c1037bee-ffe6-463c-abfc-57db5ea8d387.png', 1, 1, 2, 1)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (7, N'DT006SS', N'Samsung Galaxy S24 Ultra', N'Không phí chuyển đổi khi trả góp 0% qua thẻ tín dụng kì hạn 3-6 tháng', N'Images/Products/52b95b43-fad6-4715-8aa0-daa77a61a9d0.png', 1, 1, 2, 1)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (8, N'DT007XM', N'Xiaomi 17 Ultra', N'Không phí chuyển đổi khi trả góp 0% qua thẻ tín dụng kì hạn 3-6 tháng', N'Images/Products/9d19ef4f-f014-43f9-b849-4678583b6e26.png', 1, 1, 3, 1)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (9, N'DT008XM', N'Xiaomi 15T', N'Không phí chuyển đổi khi trả góp 0% qua thẻ tín dụng kì hạn 3-6 tháng', N'Images/Products/f283dcdf-b7a6-4f2f-92e0-3cc481ff1f49.png', 1, 0, 3, 1)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (10, N'TB009SS', N'Samsung Galaxy Tab S11 Wifi', N'Không phí chuyển đổi khi trả góp 0% qua thẻ tín dụng kì hạn 3-6 tháng', N'Images/Products/aef39fe7-bd9a-4ebf-885e-20599bfa2e87.png', 1, 1, 2, 2)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (11, N'TB010SS', N'Samsung Galaxy Tab S10 Lite Wifi', N'Không phí chuyển đổi khi trả góp 0% qua thẻ tín dụng kì hạn 3-6 tháng', N'Images/Products/16b492f8-cd94-4bb1-94ce-8fc2e94f11a6.png', 1, 0, 2, 2)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (12, N'TB011SS', N'Samsung Galaxy Tab S9', N'Không phí chuyển đổi khi trả góp 0% qua thẻ tín dụng kì hạn 3-6 tháng', N'Images/Products/ff079742-20a2-4142-a4a6-3a38dceabb3b.png', 0, 0, 2, 2)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (13, N'TB012XM', N'Xiaomi Redmi Pad 2 WiFi', N'Tặng phiếu mua hàng trị giá 300k', N'Images/Products/c20b7d83-2982-4b62-b08d-806e8f00972d.png', 1, 0, 3, 2)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (14, N'LT013AP', N'MacBook Air M4 13 inch 2025', N'Trả gop 0% - 0đ phụ phí - 0đ trả trước - kì hạn đến 12 tháng', N'Images/Products/1a838b8d-892e-43cb-ac60-1e270d041920.png', 1, 0, 4, 3)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (15, N'LT014AP', N'Laptop ASUS Vivobook 14 M1405NAQ', N'Trả gop 0% - 0đ phụ phí - 0đ trả trước - kì hạn đến 12 tháng', N'Images/Products/dacca605-561c-416c-8220-b1c89d831821.png', 1, 1, 4, 3)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (16, N'LT015AS', N'MacBook Pro M5 Pro 14 inch 2026', N'Giảm 10% khi mua kèm phụ kiện Loa, Tai nghe trị giá đến 500k', N'Images/Products/79178dd7-bb19-4f54-8ca4-4a6a9afeef44.png', 1, 1, 5, 3)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (17, N'LT016AS', N'Laptop ASUS Gaming V16 V3067VU', N'Mua laptop gaming ASUS trang bị bộ vi xử lý Intel Core', N'Images/Products/698ffd47-b989-453f-afbc-32e3777a9ec9.png', 1, 1, 5, 3)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (18, N'LT017DL', N'Laptop Dell 14 DC14250', N'Trả góp 0% lãi xuất, tối đa 12 tháng, trả trước từ 10%', N'Images/Products/88af8dd0-64b2-4b4a-8304-954aa0aa8ee6.png', 1, 0, 6, 3)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (19, N'AU018AP', N'Tai nghe Bluetooth Apple  AirPods 4 | Chính hãng', N'Trả góp 0% - 0đ phụ phí - 0đ trả trước - kì hạn 12 tháng', N'Images/Products/65f713b9-6dae-42eb-a4a1-2bb1e72e6400.png', 1, 1, 7, 4)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (20, N'AU019AP', N'Tai nghe Apple  EarPods Lighting | Chính hãng', N'Trả góp 0% - 0đ phụ phí - 0đ trả trước - kì hạn 12 tháng', N'Images/Products/d4759a26-50f2-4477-bd2f-b8f677711645.png', 0, 1, 7, 4)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (21, N'AU020AP', N'Tai nghe chụp tai chống ồn Apple AirPods Max 2 2026', N'Trả góp 0% - 0đ phụ phí - 0đ trả trước - kì hạn 12 tháng', N'Images/Products/886ac62d-8a50-47b6-94c5-fcfd92d60aa7.png', 0, 1, 7, 4)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (22, N'AU021SN', N'Tai nghe Bluetooth chụp tai Sony WH-1000X', N'Giảm 15% khi mua kèm Điện thoại hoặc Laptop', N'Images/Products/793ffe47-4ddb-4c44-a4f6-a6a1a124a8f8.png', 1, 1, 8, 4)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (23, N'AU022SN', N'Tai nghe không dây thể thao Sony Clip WF-LC900', N'Quà tặng kèm hộp đựng tai nghe ', N'Images/Products/7187b465-fd1c-4111-9b97-3b9ed2146953.png', 1, 1, 8, 4)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (24, N'AW023AP', N'Apple Watch Series 11 42mm (GPS) Viền nhôm ', N'Trả góp 0% - 0đ phụ phí - 0đ trả trước - kì hạn 12 tháng', N'Images/Products/e524955b-5017-47d7-a86a-a8213423c879.png', 1, 1, 9, 5)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (25, N'WT024SS', N'Đồng hồ Samsung Galaxy Watch 6 44mm', N'Trả góp 0% - 0đ phụ phí - 0đ trả trước - kì hạn 12 tháng', N'Images/Products/b2c966a4-c9a2-48f3-b43c-edfd24c94359.png', 0, 1, 2, 5)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (26, N'SC025AS', N'Màn hình ASUS VU249CFE 24 inch', NULL, N'Images/Products/8d89dbaf-66b8-4ce7-9aa4-32cf7747b128.png', 1, 1, 5, 6)
            INSERT [dbo].[Products] ([Id], [ProductCode], [ProductName], [Description], [UrlImageProduct], [IsFeatured], [IsOnPromotion], [BrandId], [CategoryId]) VALUES (27, N'TV026LG', N'Smart Tivi LG UHD 4K 50 inch 2026', N'Giảm thêm 1 triệu cho sản phẩm tivi trên 10 triệu ', N'Images/Products/9158f325-8d66-441d-9fe4-d84732432c59.png', 1, 1, 10, 7)
            SET IDENTITY_INSERT [dbo].[Products] OFF

            SET IDENTITY_INSERT [dbo].[Roles] ON 

            INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (1, N'Admin')
            INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (2, N'Customer')
            SET IDENTITY_INSERT [dbo].[Roles] OFF

            SET IDENTITY_INSERT [dbo].[Users] ON 

            INSERT [dbo].[Users] ([Id], [FullName], [PhoneNumber], [Email], [Password]) VALUES (1, N'Đặng Huy', N'094', N'duchuy@gmail.com', N'$2a$11$cSFLB9U90ez4.sg5O9g/Les2AAH/4DssChoTgf9JC9HXku6A7rF4C')
            SET IDENTITY_INSERT [dbo].[Users] OFF

            INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1, 2)
    
            SET IDENTITY_INSERT [dbo].[Carts] ON 

            INSERT [dbo].[Carts] ([Id], [UserId]) VALUES (1, 1)
            SET IDENTITY_INSERT [dbo].[Carts] OFF

            SET IDENTITY_INSERT [dbo].[Orders] ON 

            INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderStatus], [PaymentStatus], [PaymentMethod], [DayCreate], [TotalPrice], [UserId], [ReceiverName], [ReceiverPhone], [ShippingAddress]) VALUES (1, N'DH-3584-5/8/2026 7:55:37 AM', 0, 1, 1, CAST(N'2026-05-08T07:55:37.0164055' AS DateTime2), 61990000, 1, N'Huy Đặng', N'0941', N'abccxx')
            INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderStatus], [PaymentStatus], [PaymentMethod], [DayCreate], [TotalPrice], [UserId], [ReceiverName], [ReceiverPhone], [ShippingAddress]) VALUES (2, N'DH-1176-5/8/2026 8:49:18 AM', 0, 1, 1, CAST(N'2026-05-08T08:49:18.4957181' AS DateTime2), 61990000, 1, N'Đức Huy', N'094', N'fdfsđ')
            INSERT [dbo].[Orders] ([Id], [OrderCode], [OrderStatus], [PaymentStatus], [PaymentMethod], [DayCreate], [TotalPrice], [UserId], [ReceiverName], [ReceiverPhone], [ShippingAddress]) VALUES (3, N'DH-9242-5/8/2026 9:21:13 AM', 0, 1, 1, CAST(N'2026-05-08T09:21:13.1585220' AS DateTime2), 61990000, 1, N'đức Hy', N'094', N'fdfsf')
            SET IDENTITY_INSERT [dbo].[Orders] OFF
    
            SET IDENTITY_INSERT [dbo].[UserTokens] ON 

            INSERT [dbo].[UserTokens] ([Id], [UserId], [Token], [IsUsed], [IsRevoked], [AddedDate], [ExpiryDate], [Jti]) VALUES (1, 1, N'u4DNdMAI+jLe64MCkPFT8n/6kIE9AFXXuECjhz2t3F63Gw6RFzj1yd69/+iVvx3BBGEymco4tzpYJ34vzYUd6g==', 0, 0, CAST(N'2026-05-08T07:49:41.9833763' AS DateTime2), CAST(N'2026-05-15T07:49:41.9839245' AS DateTime2), N'930371e3-2765-4d26-9a56-9bc26f236a39')
            INSERT [dbo].[UserTokens] ([Id], [UserId], [Token], [IsUsed], [IsRevoked], [AddedDate], [ExpiryDate], [Jti]) VALUES (2, 1, N'wKean3Q0/edGJoWSqT84p+o6lxjNEsjie5tJ+mzmsxQYn1N0QPLbj3DoamCw3Se5fCuaGJjDF3ZzrQxkkbmFpg==', 0, 0, CAST(N'2026-05-08T08:48:31.7736065' AS DateTime2), CAST(N'2026-05-15T08:48:31.7738110' AS DateTime2), N'20579337-5054-48ec-9123-2c692e00960f')
            INSERT [dbo].[UserTokens] ([Id], [UserId], [Token], [IsUsed], [IsRevoked], [AddedDate], [ExpiryDate], [Jti]) VALUES (3, 1, N'bDLqGI1WQDYWQPOGiu9rS989daLbHnWxXrTZeApbM1rnyLb6Ldq4iaLhVySvRZDFZM6DAOGZlZmVveZgchc6Rw==', 0, 0, CAST(N'2026-05-08T09:18:59.5903298' AS DateTime2), CAST(N'2026-05-15T09:18:59.5909050' AS DateTime2), N'b8eb7b89-4317-4c4a-bdc1-058cb42ac742')
            SET IDENTITY_INSERT [dbo].[UserTokens] OFF

            SET IDENTITY_INSERT [dbo].[ProductVariants] ON 

            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (1, N'256GB', 37990000, 35990000, 10, N'Images/ProductColor/51086e76-9253-454c-b154-4decf0f2cc2d.png', N'Cam vu tr?', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (2, N'256GB', 37990000, 35990000, 10, N'Images/ProductColor/1eba12fb-71c8-48d3-99b9-d4d24a9b8207.png', N'Xanh đậm', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (3, N'256GB', 37990000, 35990000, 10, N'Images/ProductColor/6ff00fd9-6d1f-41db-bcbf-6d8cff914590.png', N'Bạc', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (4, N'512GB', 44490000, 44090000, 10, N'Images/ProductColor/19dd57bf-407b-490f-a26a-6d8347f45de8.png', N'Cam vũ trụ', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (5, N'512GB', 44490000, 44090000, 10, N'Images/ProductColor/3d38cbc2-314a-4842-a00c-635f9f71110b.png', N'Xanh đậm', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (6, N'512GB', 44490000, 44090000, 10, N'Images/ProductColor/101a6567-1ab5-4e05-a446-91c3a1f1d000.png', N'Bạc', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (7, N'1TB', 50990000, 50090000, 10, N'Images/ProductColor/f9f1ce86-dbd0-457f-89c2-c1d887e56d35.png', N'Bạc', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (8, N'1TB', 50990000, 50090000, 10, N'Images/ProductColor/5e4ddb82-1539-4f44-8e70-096576e6f21d.png', N'Xanh đậm', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (9, N'1TB', 50990000, 50090000, 10, N'Images/ProductColor/cfc4e2ed-0a85-44e4-81b6-4f331d438879.png', N'Cam vũ trụ', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (10, N'2TB', 63990000, 61990000, 10, N'Images/ProductColor/de17cedf-ddcc-43ac-8fcc-719154b23aab.png', N'Cam vũ trụ', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (11, N'2TB', 63990000, 61990000, 10, N'Images/ProductColor/796ce7e2-4bb1-4376-b75b-040521530505.png', N'Xanh đậm', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (12, N'2TB', 63990000, 61990000, 10, N'Images/ProductColor/f1428ec7-55b4-4b0f-a357-7ae1860168ae.png', N'Bạc', 1)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (13, N'256GB', 34990000, 30990000, 10, N'Images/ProductColor/a1eabde6-5204-4f60-b9fd-d7f82f9ba058.png', N'Titan tự nhiên', 2)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (14, N'256GB', 34990000, 30990000, 10, N'Images/ProductColor/e2e2fe55-f5a4-4f0e-b4cd-629f88833645.png', N'Titan Đen', 2)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (15, N'256GB', 34990000, 30990000, 10, N'Images/ProductColor/b08c90a0-2f57-4282-892c-46683be99a1d.png', N'Titan Sa Mạc', 2)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (16, N'512GB', 40990000, 37990000, 10, N'Images/ProductColor/7fa6f45d-635a-4bad-b8d4-a519a0496c93.png', N'Titan Sa Mạc', 2)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (17, N'512GB', 40990000, 37990000, 10, N'Images/ProductColor/3d72c8a9-31d6-41a6-a830-b1facfbe85bf.png', N'Titan Đen', 2)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (18, N'512GB', 40990000, 37990000, 10, N'Images/ProductColor/51165a6f-c67e-49b6-b573-78840917e8d5.png', N'Titan tự nhiên', 2)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (19, N'1TB', 46990000, 42990000, 10, N'Images/ProductColor/48413827-4322-41b0-8f1e-284dfb5a603c.png', N'Titan tự nhiên', 2)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (20, N'1TB', 46990000, 42990000, 10, N'Images/ProductColor/e8a999bc-ad1b-4a93-9ea2-886f828c9e93.png', N'Titan Đen', 2)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (21, N'1TB', 46990000, 42990000, 10, N'Images/ProductColor/d0ebfc37-d312-4f3c-9ea0-1e862004b9b2.png', N'Titan Sa Mạc', 2)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (22, N'256GB', 34990000, 29990000, 10, N'Images/ProductColor/3760dbfe-84c3-4e6f-aee4-1d0736ee59f8.png', N'Titan Đen', 3)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (23, N'256GB', 34990000, 29990000, 10, N'Images/ProductColor/08dd8931-7899-41f3-8d98-a79d47ca781f.png', N'Titan Trắng', 3)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (24, N'512GB', 40990000, 28490000, 10, N'Images/ProductColor/aa6414e0-36b4-4b95-be24-3ae13210f7e9.png', N'Titan Trắng', 3)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (25, N'512GB', 40990000, 28490000, 10, N'Images/ProductColor/20889e4b-58bb-4667-9b3f-4f70825b4297.png', N'Titan Đen', 3)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (26, N'1TB', 46990000, 31990000, 10, N'Images/ProductColor/c71920e7-92b8-4999-a0a0-6bb0c53d1606.png', N'Titan Đen', 3)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (27, N'1TB', 46990000, 31990000, 10, N'Images/ProductColor/4a9330a5-d291-4664-8531-bc8d2bbe52b1.png', N'Titan Trắng', 3)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (28, N'S23 Ultra 12GB 256GB', 36990000, 32490000, 10, N'Images/ProductColor/a4513028-391a-4a7f-97d7-ea30b3672a6c.png', N'Đen Classic', 4)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (29, N'S23 Ultra 12GB 256GB', 36990000, 32490000, 10, N'Images/ProductColor/e954d1d9-e961-42c9-86c5-b9ed49b09904.png', N'Tím Cobalt', 4)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (30, N'S23 Ultra 12GB 512GB', 42990000, 38490000, 10, N'Images/ProductColor/cca145d9-0b99-4daf-a4d7-ecfe6966c890.png', N'Tím Cobalt', 4)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (31, N'S23 Ultra 12GB 512GB', 42990000, 38490000, 10, N'Images/ProductColor/65e71ae9-5940-4486-9853-03d7ccf4fa42.png', N'Đen Classic', 4)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (32, N'S23 Ultra 16GB 1TB', 51990000, 47490000, 10, N'Images/ProductColor/0f7b9e97-63f6-4ad5-a069-2cd1c1fadfa3.png', N'Đen Classic', 4)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (33, N'S25 Ultra 256GB', 33380000, 27490000, 10, N'Images/ProductColor/c977c049-5ca4-4f00-b99d-02c67e5d3386.png', N'Đen', 5)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (34, N'S25 Ultra 512GB', 39490000, 31180000, 10, N'Images/ProductColor/4a8dc85a-4497-4679-9607-8d5cb2cec44b.png', N'Đen', 5)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (35, N'S25 Ultra 1TB', 43950000, 35490000, 10, N'Images/ProductColor/a5f55a1a-1db7-4197-83cd-0b884cc78607.png', N'Đen', 5)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (36, N'16GB 512GB', 39990000, 32470000, 10, N'Images/ProductColor/4add8028-d6e6-4fb2-9050-e8cce03d9cad.png', N'Đen', 8)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (37, N'16GB 512GB', 29940000, 18990000, 10, N'Images/ProductColor/bc4a8f9c-4531-496a-8d7a-36d789c99035.png', N'Tím', 7)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (38, N'15T', 14990000, 13490000, 10, N'Images/ProductColor/9d51ed62-c7e7-4fb4-9a05-ba0f05aec608.png', N'Vàng Hồng', 9)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (39, N'12GB 256GB', 20990000, 19790000, 10, N'Images/ProductColor/84f894c8-eac3-4b0b-9aba-a2daafa1711d.png', N'Bạc', 10)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (40, N'12GB 256GB', 8990000, 8290000, 10, N'Images/ProductColor/c4cc4542-72cd-401b-a776-22355559c58f.png', N'Xam', 10)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (41, N'12GB 256GB', 8990000, 8290000, 10, N'Images/ProductColor/8903f394-c2fc-4831-8e96-c2b7352fc6e5.png', N'Xam', 11)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (42, N'Pad 2Pro 5G', 8990000, 8290000, 10, N'Images/ProductColor/0c4f04fb-931e-4a54-b7d9-109999f10837.png', N'Xám', 12)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (43, N'Pad 2Pro 5G', 8990000, 8290000, 10, N'Images/ProductColor/87c09c68-d8e9-4e65-a1e5-7343a96dbc37.png', N'Xám', 13)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (44, N'10CPU - 10GPU - 16GB - 1TB', 36990000, 34990000, 10, N'Images/ProductColor/07c06a04-3060-4545-9db8-319299bb75f3.png', N'Xanh da trời', 14)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (45, N'10CPU - 10GPU - 16GB - 1TB', 36990000, 34990000, 10, N'Images/ProductColor/fc4aafeb-154b-4606-927b-8e985d77564c.png', N'Xanh da trời', 16)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (46, N'16GB', 36990000, 34990000, 10, N'Images/ProductColor/a626ddc0-5fc7-4081-83dc-90c3529da94d.png', N'Bạc', 15)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (47, N'16GB', 36990000, 34990000, 10, N'Images/ProductColor/533733d9-0243-4ff1-942b-048b526ad7de.png', N'Bạc', 17)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (48, N'16GB', 36990000, 34990000, 10, N'Images/ProductColor/0794b2b3-8d6b-43ed-a5fb-a079ea51542e.png', N'Bạc', 18)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (49, N'AirPods 4 Chống ồn chủ động', 3790000, 2990000, 10, N'Images/ProductColor/497c424b-7b07-47db-a0db-1c76277d66ce.png', N'Trắng ', 19)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (50, N'Lightning', 790000, 500000, 10, N'Images/ProductColor/970e16cf-9234-4635-8e42-6b65fd0dbe24.png', N'Trắng ', 20)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (51, N'AirPods Max 2024', 1490000, 1319000, 10, N'Images/ProductColor/ed9c9c3c-d132-4998-aa5a-64c79f471ce8.png', N'Tím', 21)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (52, N'Clip WF-LC900', 490000, 319000, 10, N'Images/ProductColor/950373dd-bb9d-412e-8fc1-85eca1611d0c.png', N'Tím', 22)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (53, N'Clip WF-LC900', 490000, 319000, 10, N'Images/ProductColor/ba510869-0642-4264-ba42-ecfabf40f3b9.png', N'Tím', 23)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (54, N'46mm 5G Dây thép Size S - M', 23990000, 21590000, 10, N'Images/ProductColor/df23438f-e11e-41eb-8887-224d1e52b671.png', N'Xám', 24)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (55, N'44mm Bluetooth', 7490000, 4090000, 10, N'Images/ProductColor/b95f4a4f-c8c9-49a1-b349-1a8397c40af1.png', N'Xám', 25)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (56, N'32 inch - 4K IPS - 60Hz', 12209000, 9209000, 10, N'Images/ProductColor/65698f88-079f-41d9-8ffa-32c176c12f27.png', N'Xám', 26)
            INSERT [dbo].[ProductVariants] ([Id], [Storage], [OriginPrice], [CurrentPrice], [Stock], [UrlProductColor], [ColorName], [ProductId]) VALUES (57, N'60 inch', 19209000, 14209000, 10, N'Images/ProductColor/a5958ba1-faf7-44c4-b52f-a9ae39f0f561.png', N'Xám', 27)
            SET IDENTITY_INSERT [dbo].[ProductVariants] OFF

            SET IDENTITY_INSERT [dbo].[OrderItems] ON 

            INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductVariantId], [PriceAtTime], [Quantity], [CreateAt]) VALUES (1, 1, 11, 61990000, 1, CAST(N'2026-05-08T07:55:37.0178553' AS DateTime2))
            INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductVariantId], [PriceAtTime], [Quantity], [CreateAt]) VALUES (2, 2, 10, 61990000, 1, CAST(N'2026-05-08T08:49:18.4971501' AS DateTime2))
            INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductVariantId], [PriceAtTime], [Quantity], [CreateAt]) VALUES (3, 3, 10, 61990000, 1, CAST(N'2026-05-08T09:21:13.1591507' AS DateTime2))
            SET IDENTITY_INSERT [dbo].[OrderItems] OFF
             ";

            try
            {
                await context.Database.ExecuteSqlRawAsync(sqlData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
