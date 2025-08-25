import express from 'express';
import { getPrivateKey, MintTokensToUser } from '../controllers/MintTokens';

const router: express.Router = express.Router();

router.post('/minttokens', MintTokensToUser)

export default router;