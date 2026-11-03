# MVP ERD (Continuation Requirement)

```text
users (AspNetUsers) (1) ----< user_customers >---- (1) customers
users (1) ----< movements >---- (1) machines
garment_models (1) ----< machine_stock (MovMaquinaDisAlmacenes) >---- (1) machines
subclients (MasterSubClientes) (1) ----< subclient_stock (MovStockPrendasUsu)
users/subclients (1) ----< device_tokens
users/subclients (1) ----< support_tickets >---- (0..1) machines

movements (transaction rows)
  -> on collect/return, atomically updates:
     - machine_stock (MovMaquinaDisAlmacenes.StockReal)
     - subclient_stock (MovStockPrendasUsu.Cantidad, when token subject is sc:*)
```

## API to Table Mapping

- `POST /api/auth/login`: `AspNetUsers`
- `POST /api/auth/subcliente/login`: `MasterSubClientes`
- `POST /api/auth/refresh`: in-memory refresh session store
- `POST /api/auth/logout`: in-memory refresh session store revoke
- `GET /api/me`: `AspNetUsers`, `AppUserCustomers`, `MasterClientes`, `MasterSubClientes`
- `GET /api/me/credits`: `MovCreditosModelosDepartamentos` / `MovStockPrendasUsu`, `MovCreditosDepartamentos`
- `GET /api/me/sizes`, `PUT /api/me/sizes`: `MovModeloTallaSub`, `MasterModeloPrenda`
- `GET /api/me/movements`: `Movements`
- `POST /api/transactions`: `Movements` + `MovMaquinaDisAlmacenes` (+ `MovStockPrendasUsu` for subcliente) in one DB transaction
- `POST /api/transactions/{id}/cancel`: reverse stock/credits + mark original movement rows as deleted
- `POST /api/notifications/register`: `DeviceTokens`
- `POST /api/support/tickets`: `SupportTickets`
