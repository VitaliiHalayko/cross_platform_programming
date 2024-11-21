const http = require('http');
const logger = require('../config/logger');
const { getUserTokens } = require('./test_helper');
const request = require('supertest');

const PORT = 5230;
const BASE_PATH = '/dbapi';

describe('ProductController Tests', () => {
    let access_token;

    const tokens = getUserTokens();
    access_token = tokens.access_token;

    afterAll(() => {
        logger.info('Finished ProductController tests');
    });

    test('GET /Product should return a list of products', async () => {
        logger.info('Testing GET /Product endpoint');

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/Product/Product`,
            method: 'GET',
            headers: {
                Authorization: `Bearer ${access_token}`,
            },
            agent: new http.Agent({
                rejectUnauthorized: false
            }),
            timeout: 10000,
        };
        
        const req = http.request(options, (res) => {
            let data = '';

            res.on('data', (chunk) => {
                data += chunk;
            });

            res.on('end', () => {
                expect(res.statusCode).toBe(200);

                try {
                    const parsedData = JSON.parse(data);
                    expect(Array.isArray(parsedData)).toBe(true);
                } catch (error) {
                    logger.error('Error parsing response data');
                }

                done();
            });
        });

        req.on('error', (error) => {
            logger.error(`Error testing GET /Product: ${error.message}`);
            done(error);
        });

        req.end();
    });

    test('GET /Product/:id should return a product by ID', async () => {
        logger.info('Testing GET /Product/:id endpoint');

        const productId = 1;

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/Product/Product/${productId}`,
            method: 'GET',
            headers: {
                Authorization: `Bearer ${access_token}`,
            },
            agent: new http.Agent({
                rejectUnauthorized: false,
            }),
            timeout: 10000,
        };

        const req = http.request(options, (res) => {
            let data = '';

            res.on('data', (chunk) => {
                data += chunk;
            });

            res.on('end', () => {
                logger.info(`Received product by ID: ${data}`);

                expect(res.statusCode).toBe(200);

                try {
                    const parsedData = JSON.parse(data);
                    expect(parsedData).toHaveProperty('ProductId');
                    expect(parsedData.ProductId).toBe(productId);
                } catch (error) {
                    logger.error('Error parsing response data');
                }

                done();
            });
        });

        req.on('error', (error) => {
            logger.error(`Error testing GET /Product/:id: ${error.message}`);
            done(error);
        });

        req.end();
    });
});
