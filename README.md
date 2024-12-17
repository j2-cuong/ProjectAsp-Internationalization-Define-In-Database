# ProjectAsp-Internationalization-Define-In-Database
# 📄 Documentation

## 🛠️ Công nghệ sử dụng

### 1. **🛠️ASP.NET + SQL Database**
Ứng dụng được xây dựng bằng **ASP.NET** để tạo các trang web động và **SQL** để quản lý cơ sở dữ liệu hiệu quả.

### 2. **📊ADO.NET**
Sử dụng **ADO.NET** để xử lý kết nối và truy vấn dữ liệu nhanh chóng, hiệu quả từ cơ sở dữ liệu SQL.

### 3. **🔧Dependency Injection (DI)**
Ứng dụng áp dụng kỹ thuật **Dependency Injection** để quản lý các phụ thuộc, giúp mã nguồn dễ bảo trì và kiểm thử hơn.

### 4. **📑Stored Procedure**
Sử dụng **Stored Procedure** cho các truy vấn phức tạp, nhằm tối ưu hóa hiệu suất và đảm bảo an toàn dữ liệu.

### 5. **🛡️Bảo mật chống SQL Injection**
Tất cả các truy vấn SQL sử dụng **Parameters** để phòng chống tấn công SQL Injection, đảm bảo tính bảo mật.

### 6. **⚡IMemoryCache**
Dữ liệu được lưu trữ tạm thời trong **IMemoryCache** nhằm cải thiện hiệu suất truy xuất dữ liệu.

---

## 📂 Cấu trúc thư mục

### 1. **🏠 HomeController**
Quản lý các sự kiện chính:
- **ChangeLanguage**: Chọn ngôn ngữ trên giao diện và lưu vào cookies.
- **GetLanguageConfig**: Lấy danh sách các ngôn ngữ được khai báo trong bảng `LanguageConfig`.

> **Sử dụng Dependency Injection**: Inject `ICookiesRepository` và `IDatabaseLocalizer` vào constructor.

### 2. **📚 Documentation**
- **Documentation**: Tài liệu hướng dẫn sử dụng ứng dụng.
- **Script**: Mô tả các script tương tác với cơ sở dữ liệu.

### 3. **🧩 Models**
Khai báo cấu trúc các **model** tương ứng với các bảng trong cơ sở dữ liệu.

### 4. **🛠️ Helper**
- **CheckNewestData**: Kiểm tra sự thay đổi dữ liệu trong bảng `Translation`.
  - Kiểm tra cột `IsChangeData` trong bảng `LanguageConfig`. Nếu giá trị là `true`, cần xử lý lại cache.

### 5. **📡 Services**
- **Cookies**:
  - Chứa interface và logic xử lý cookies.
  - Lưu ngôn ngữ được chọn vào cookies để sử dụng trong các lần tải giao diện sau.

- **DatabaseLocalizer**:
  - Chứa interface và logic xử lý DatabaseLocalizer.
  - Query cơ sở dữ liệu để lấy danh sách ngôn ngữ và các bản dịch dựa trên ngôn ngữ và key giao diện.

### 6. **🌐 Views**
- **_ViewImports.cshtml**:
  - Khai báo các namespace hoặc thuộc tính dùng chung trong các tệp `.cshtml`.
  - Inject `IDatabaseLocalizer` để xử lý các logic liên quan đến bản dịch từ cơ sở dữ liệu.
- **Home/Index.cshtml**:
  - Chứa logic chuyển đổi ngôn ngữ.
- **Shared/_Layout.cshtml**:
  - Chứa script và HTML cho dropdown chọn ngôn ngữ.

### 7. **📝 Program.cs**
- Cấu hình các services cần thiết cho ứng dụng, bao gồm:
  - **ICookiesRepository**
  - **IDatabaseLocalizer**
  - **IMemoryCache**
  - **Dependency Injection**.