# Pharma.API

A **modern ASP.NET Core Web API** built with **.NET 8** and **C# 12** for managing pharmacy orders.  
This project demonstrates clean API architecture with **filtering, sorting, pagination, caching, and structured logging**.  

---

## 🚀 Getting Started  

Run the project with:  

```bash
dotnet run
```  

---

## 🔍 Observability & Tooling  

- **Windows Terminal** for real-time logs:  
  <img width="826" height="488" alt="logs" src="https://github.com/user-attachments/assets/e0d8b849-650b-470e-ba8e-43813e854ccf" />  

- **Postman** for API testing:  
  <img width="1138" height="1031" alt="postman" src="https://github.com/user-attachments/assets/e05dc1fc-0538-4724-8ead-22f3f829d372" />  

- **Swagger UI** for interactive API docs:  
  <img width="1506" height="906" alt="swagger" src="https://github.com/user-attachments/assets/6a830498-311f-4ae1-9cfa-8a686f523964" />  

---

## ✨ Features  

- Advanced **filtering, sorting, and pagination** for pharmacy orders  
- **In-memory caching** for demo data  
- **Structured logging** for observability  
- Extensible and modular **controller/service architecture**  

---

## 📌 API Endpoints  

### `GET /orders`  

Returns a paginated list of orders.  

**Query Parameters:**  

| Parameter    | Type       | Required | Default    | Description |
|--------------|-----------|----------|------------|-------------|
| `pharmacyId` | string    | No       | –          | Filter by pharmacy |
| `status`     | array     | No       | –          | Filter by one or more statuses |
| `from`       | DateTime  | No       | –          | Filter by start date |
| `to`         | DateTime  | No       | –          | Filter by end date |
| `sortBy`     | string    | No       | `createdAt`| Sort by `createdAt` or `totalCents` |
| `dir`        | string    | No       | `desc`     | Sort direction (`asc` / `desc`) |
| `page`       | int       | No       | 1          | Page number |
| `pageSize`   | int       | No       | 20         | Items per page (max 100) |

---

## ⚡ Performance & Scaling  

### 1. Filtering  

Supported fields:  
- `PharmacyId`  
- `Status` (multi-select)  
- `CreatedAt` (date range)  

**Recommended index:**  

```sql
CREATE INDEX IX_Orders_Pharmacy_Status_CreatedAt 
ON Orders (PharmacyId, Status, CreatedAt);
```  

### 2. Sorting  

- Default: `CreatedAt`  
- Optional: `TotalCents`  

**Additional index for frequent `TotalCents` sorting:**  

```sql
CREATE INDEX IX_Orders_TotalCents 
ON Orders (TotalCents);
```  

### 3. Pagination  

- Uses **`page` + `pageSize`** (`OFFSET / FETCH` in SQL).  
- Efficient for small/medium ranges, but performance may degrade on very large offsets.  

---

## 🧪 Testing Strategy  

### Unit Tests  
- Validate filters (`PharmacyId`, `Status[]`, date ranges)  
- Validate pagination (`page`, `pageSize`)  

### Integration Tests  
- Seed thousands of orders  
- Ensure:  
  - Correct item counts  
  - Accurate metadata (`TotalCount`, `TotalPages`)  
  - Proper sorting  

### Performance Tests  
- Stress test with large datasets  
- Monitor **DB CPU and I/O** under load  

---

✅ Clean, extensible, and production-ready API foundation for pharmacy order management.  
