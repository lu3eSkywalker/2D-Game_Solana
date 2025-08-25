import express from 'express';
import Server = require('../controllers/Server');
import BackendHealth = require('../controllers/Server');

const router: express.Router = express.Router();

router.get('/', Server.BackendCheck)
router.get('/health', Server.BackendHealth)

export default router;