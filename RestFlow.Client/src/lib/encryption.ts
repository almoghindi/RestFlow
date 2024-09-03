const encryptionKey = "your-encryption-key";

export function encryptData(data: string): string {
  try {
    const encrypted = btoa(
      data
        .split("")
        .map((char, i) =>
          String.fromCharCode(
            char.charCodeAt(0) ^
              encryptionKey.charCodeAt(i % encryptionKey.length)
          )
        )
        .join("")
    );
    return encrypted;
  } catch (e) {
    return "";
  }
}

export function decryptData(encryptedData: string): string {
  try {
    const decrypted = atob(encryptedData)
      .split("")
      .map((char, i) =>
        String.fromCharCode(
          char.charCodeAt(0) ^
            encryptionKey.charCodeAt(i % encryptionKey.length)
        )
      )
      .join("");
    return decrypted;
  } catch (e) {
    return "";
  }
}
