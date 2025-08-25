import { Request, Response } from "express";
import { bs58 } from "@coral-xyz/anchor/dist/cjs/utils/bytes";
import { Keypair } from "@solana/web3.js";
import * as anchor from "@coral-xyz/anchor";
import * as web3 from "@solana/web3.js";
import {
  CpiGuardLayout,
  createAssociatedTokenAccountInstruction,
  getAccount,
  getAssociatedTokenAddress,
} from "@solana/spl-token";
// import dotenv from "dotenv";
import nacl from "tweetnacl";
import idl from "../idl/spl_token_mint_and_metadata.json";
import bs58 from "bs58";

// dotenv.config();

const privateKeyBase58 = "PrivateKey_Of_User_To_Sign_Transactions";

const privateKeyUint8Array = bs58.decode(privateKeyBase58);

const userKeypair = Keypair.fromSecretKey(privateKeyUint8Array);

console.log(
  "This is the fucking Public Key: ",
  userKeypair.publicKey.toBase58()
);

const userPublicKey = new web3.PublicKey(userKeypair.publicKey.toBase58());

const connection = new web3.Connection(
  "https://devnet.helius-rpc.com/?api-key=your_api_key",
  "confirmed"
);
const userWallet = new anchor.Wallet(userKeypair);
const provider = new anchor.AnchorProvider(connection, userWallet, {
  preflightCommitment: "confirmed",
});
anchor.setProvider(provider);

const programId = new web3.PublicKey(
  "D7w1k3XCxg8MXwuR84ZdCuG6sReMLx2NVKEyVSUmuErE"
);

const program = new anchor.Program(idl as anchor.Idl, provider);

const [mint] = web3.PublicKey.findProgramAddressSync(
  [Buffer.from("mint")],
  program.programId
);

const [authority, bump] = web3.PublicKey.findProgramAddressSync(
  [Buffer.from("authority")],
  program.programId
);

export const MintTokensToUser = async (
  req: Request,
  res: Response
): Promise<void> => {
  try {
    const userAddress = new web3.PublicKey(
      "74wdHWQQRSXHosQsMZKkk7YKpYUZdt5RZUDDGPjoVQ9"
    );

    const destination = await getAssociatedTokenAddress(mint, userAddress);

    console.log("This is the userATA: ", destination.toBase58());

    // This try-catch block checks if the user ATA is initialized or not
    try {
      const ataAccountInfo = await connection.getAccountInfo(destination);

      if (!ataAccountInfo || ataAccountInfo == null) {
        console.log("ATA is not initialized. Initializing..");

        // Create associated token account if it doesn't exit
        const ataIx = createAssociatedTokenAccountInstruction(
          userPublicKey,
          destination,
          userAddress,
          mint
        );

        const tx = new web3.Transaction().add(ataIx);

        const txid = await connection.sendTransaction(tx, [userKeypair], {
          preflightCommitment: "confirmed",
        });

        const confirmation = await connection.confirmTransaction(
          txid,
          "confirmed"
        );
        console.log("Transaction confirmed: ", confirmation);

        // await program.provider.sendAndConfirm(tx);

        res.status(200).json({
          success: true,
          message: "Token ata Successfull",
        });
      }
    } catch (error) {
      console.log("Error", error);
      res.status(500).json({
        success: false,
        message: "Token Account Initialization Error",
      });
    }

    const txHash = await program.methods
      .mintTokens(new anchor.BN(100_000_000_000))
      .accounts({
        mint,
        authority,
        destination,
        destinationOwner: userAddress,
        payer: userWallet.publicKey,
        rent: web3.SYSVAR_RENT_PUBKEY,
        systemProgram: web3.SystemProgram.programId,
        tokenProgram: new web3.PublicKey(
          "TokenkegQfeZyiNwAJbNbGKPFXCWuBvf9Ss623VQ5DA"
        ),
        associatedTokenProgram: anchor.utils.token.ASSOCIATED_PROGRAM_ID,
      })
      .signers([userKeypair])
      .rpc();

    console.log(`Use 'solana confirm -v ${txHash}' to see the logs`);

    // Confirm Transaction
    await connection.confirmTransaction(txHash);

    res.status(200).json({
      success: true,
      message: "Token Minting Successfull",
    });
  } catch (error) {
    console.log("Error", error);
    res.status(500).json({
      success: false,
      maessage: "Token Minting Error",
    });
  }
};