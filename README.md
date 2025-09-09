# 2D-Game_Solana

# Flappy Bird Token 🕊️💰  

A fun twist on the classic **Flappy Bird** game – every time you play, you can **mint Flappy Bird Tokens (FBT)** on **Solana**! 🎮✨  

This project combines **Unity (C#)**, a **Solana Token Mint Program**, and a **Backend service** to bring Web3 rewards into gaming.  

---

## 🚀 Features  
- 🎮 **Classic Flappy Bird Gameplay** built in **Unity (C#)**  
- 🪙 **Custom Solana Token Mint Program** – deployed to the Solana blockchain  
- 🌐 **Backend Service** – mints tokens on behalf of the player  
- 🔗 **Unity ↔ Backend Integration** – the game sends requests to mint tokens  

---

## Gameplay

<video src="https://github.com/user-attachments/assets/8c9b215f-3946-4266-96a4-0965202ce410" width="500" controls></video>

---

## 📂 Project Structure  

---

## 🛠️ Tech Stack  
- **Game:** Unity + C#  
- **Blockchain:** Solana (Rust Program)  
- **Backend:** Node.js / Express  
- **Token Standard:** Solana SPL Token  

---

## ⚙️ How It Works  
1. **Player plays the game** in Unity.  
2. On certain event (e.g., scoring points), Unity sends a request to the backend.  
3. **Backend** calls the Solana **TokenMint_Program** to mint new tokens.  
4. Tokens are sent directly to the **user’s Solana wallet**. 🎉  

---

## 🔧 Setup & Installation  

### 1️⃣ Clone the Repo  
```bash
git clone https://github.com/lu3eSkywalker/2D-Game_Solana
cd 2D-Game_Solana
