# sec-exercise

「密碼學與網路安全」課程的課堂練習集，使用 C# Windows Forms（.NET Framework）開發，以 Visual Studio 開啟 `sec-exercise.sln` 即可建置所有專案。

## 專案列表

| 專案 | 主題 | 說明 |
|------|------|------|
| `hash01` | 雜湊與對稱式加密 | SHA-256 雜湊計算，以及 DES（CBC 模式、PKCS7 填充）加解密 |
| `RSA01` | 非對稱式加密 | 產生 RSA 金鑰對並存成 XML 檔（`key/pub.xml`、`key/pvt.xml`），用公鑰加密、私鑰解密 |
| `server01` | Socket 通訊（伺服端） | 多執行緒 TCP Server，監聽 port 15000，接收訊息並回覆 |
| `client01` | Socket 通訊（客戶端） | TCP Client，連線至 Server 送出訊息並顯示回覆 |
| `DBLogin` | 資料庫操作 | SQLite 使用者資料表的建立、新增與查詢，使用參數化查詢防止 SQL Injection |
| `GTOTP` | 兩步驟驗證 | 產生 TOTP Secret Key 與 otpauth QR Code，供 Google Authenticator 掃描並驗證 6 位數動態密碼 |
| `term-project` | 期末專題 | 綜合應用：註冊/登入系統，密碼以 HMAC-SHA256 雜湊後存入 SQLite，並整合 TOTP 兩步驟驗證（含固定時間比較防止 timing attack） |

## 使用的套件

- Otp.NET 1.4.1 — TOTP 產生與驗證
- QRCoder 1.8.0 — QR Code 產生
- System.Data.SQLite 2.0.3 — SQLite 資料庫
- EntityFramework 6.4.4

## 執行方式

1. 以 Visual Studio 開啟 `sec-exercise.sln`
2. 還原 NuGet 套件
3. 選擇要執行的專案設為啟動專案，按 F5 執行

測試 Socket 通訊時，先啟動 `server01` 再執行 `client01`（本機測試 IP 填 `127.0.0.1`）。

> 注意：本專案僅供課堂練習。DES 已不安全，實務上請改用 AES；密碼儲存實務上建議使用 bcrypt/PBKDF2 等專用演算法。
