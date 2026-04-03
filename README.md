# Library Management System

Bu layihə **ASP.NET WebForms**, **ASMX Web Service**, **SQL Server** və **ADO.NET** istifadə edilərək hazırlanmış **Kitabxana İdarəetmə Sistemi**dir.

## Layihənin məqsədi

Bu layihənin əsas məqsədi kitabxanadakı əsas prosesləri avtomatlaşdırmaqdır. Sistem vasitəsilə:

- kitabların əlavə olunması
- oxucuların əlavə olunması
- kitabların siyahılanması
- oxucuların siyahılanması
- kitabın oxucuya verilməsi
- kitabın geri qaytarılması

əməliyyatları həyata keçirilir.

Bu layihə aşağıdakı texnologiyalarla işləmək üçün hazırlanmışdır:

- ASP.NET WebForms
- Web Service
- XML və ya JSON formatında məlumat mübadiləsi
- SQL Server ilə işləmə

---

## İstifadə olunan texnologiyalar

- **ASP.NET WebForms**
- **C#**
- **ASMX Web Service**
- **SQL Server**
- **ADO.NET**
- **Master Page**
- **GridView**
- **TextBox / DropDownList / Button**

---

## Layihənin strukturu

Layihə 3 əsas hissədən ibarətdir:

### 1. Presentation Layer
İstifadəçi interfeysi hissəsidir.

Səhifələr:
- `Books.aspx`
- `Readers.aspx`
- `Borrow.aspx`

### 2. Service Layer
Web Service hissəsidir.

Fayl:
- `LibraryService.asmx`

Servis metodları:
- `GetAllBooks()`
- `AddBook()`
- `GetAllReaders()`
- `AddReader()`
- `BorrowBook()`
- `ReturnBook()`
- `GetBorrowedBooks()`

### 3. Data Layer
Verilənlər bazası hissəsidir.

SQL Server cədvəlləri:
- `Books`
- `Readers`
- `BorrowedBooks`

---

## Əsas funksiyalar

### Kitablar
- yeni kitab əlavə etmək
- kitabların siyahısını göstərmək

### Oxucular
- yeni oxucu əlavə etmək
- oxucuların siyahısını göstərmək

### Kitabın verilməsi
- kitabın oxucuya verilməsi
- geri qaytarılması
- verilmiş kitabların siyahısına baxılması

---

## Verilənlər bazası cədvəlləri

### Books
- `BookId`
- `Title`
- `Author`
- `Category`
- `PublishYear`
- `Quantity`
- `AvailableCount`

### Readers
- `ReaderId`
- `FullName`
- `Phone`
- `Email`
- `Address`

### BorrowedBooks
- `BorrowId`
- `BookId`
- `ReaderId`
- `BorrowDate`
- `ReturnDate`
- `IsReturned`

---

## Məlumat mübadiləsi

Bu layihədə **Web Service** istifadə olunmuşdur.  
Servis metodları `[WebMethod]` vasitəsilə təqdim olunur və məlumat mübadiləsi əsasən **XML formatında** həyata keçirilir.

---

## Master Page istifadəsi

Layihədə bütün səhifələr üçün ümumi görünüş və naviqasiya təmin etmək məqsədilə **Master Page** istifadə olunmuşdur.

Header hissəsində aşağıdakı keçidlər mövcuddur:

- Ana səhifə
- Kitablar
- Oxucular
- Kitab verilməsi

Bu yanaşma layihənin daha səliqəli və professional görünməsini təmin edir.

---

## Layihənin iş prinsipi

1. İstifadəçi WebForms səhifəsinə daxil olur
2. Məlumat daxil edir
3. Səhifə Web Service metodunu çağırır
4. Web Service SQL Server ilə əlaqə yaradır
5. Məlumat bazaya yazılır və ya bazadan oxunur
6. Nəticə istifadəçiyə göstərilir

---

## Quraşdırma

### 1. Verilənlər bazasını yarat
SQL Server-də yeni database yarat:

```sql
LibrarySystemDB
