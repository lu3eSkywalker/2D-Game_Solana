import { Request, Response } from "express";
import { bs58 } from "@coral-xyz/anchor/dist/cjs/utils/bytes";
import * as anchor from "@coral-xyz/anchor";
import * as web3 from "@solana/web3.js";
import dotenv from 'dotenv';
dotenv.config();

export const BackendCheck = async(req: Request, res: Response): Promise<void> => {
    try {
        res.send(
            "Hello, World! 🚀 Backend is running..."
        );

    }
    catch(error) {
        console.log('Error: ', error)
        res.status(500).json({
            success: false,
            message: 'Backend Error'
        })
    }
}

export const BackendHealth = async(req: Request, res: Response): Promise<void> => {
    try {
        res.json({
            status: "OK",
            timestamp: new Date().toISOString()
        });
    }
    catch(error) {
        console.log('Error: ', error)
        res.status(500).json({
            success: false,
            message: 'Backend Error'
        })
    }
}