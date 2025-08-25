// index.ts
import express, { Request, Response } from "express";
import dotenv from "dotenv";
import backendRoutes from './routes/Routes';

dotenv.config();

const app = express();
const PORT: number = parseInt(process.env.PORT || "3000", 10);

// Middleware
app.use(express.json());
app.use('/api/v1', backendRoutes);

// Start server
app.listen(PORT, () => {
  console.log(`✅ Server is running at http://localhost:${PORT}`);
});