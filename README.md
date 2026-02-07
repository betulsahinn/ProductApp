# ProductApp – WinForms CRUD Application

ProductApp is a simple **Windows Forms CRUD application** built with **C# and .NET**.
The project is intentionally kept minimal to demonstrate **clean architecture**,  
**correct state management**, and a reliable **CRUD workflow** with a real database.

The focus is on correctness and clarity rather than complex UI or excessive features.

---

## ✨ Features

- Create, read, update, and delete products (CRUD)
- Real SQL Server database integration
- DataGridView used as read-only for listing and selection
- Controlled editing via form inputs
- Basic validation and user feedback
- Layered architecture (UI / Service / Data)

---

## 🧱 Technologies

- C#
- .NET 6 / .NET 8
- Windows Forms (WinForms)
- SQL Server Express 2022
- ADO.NET (Microsoft.Data.SqlClient)

---

## 🧠 Design Notes

- Grid editing is intentionally disabled; all updates are handled via the form
- Business rules are handled in the service layer
- The project serves as a solid foundation for more advanced desktop or backend-focused applications

---
