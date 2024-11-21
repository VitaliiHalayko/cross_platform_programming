const http = require('http');
const logger = require('../config/logger');
const { getUserTokens } = require('./test_helper');
const request = require('supertest');

const PORT = 5230;
const BASE_PATH = '/dbapi';

describe('ReferenceController Tests', () => {
    let access_token;

    const tokens = getUserTokens();
    access_token = tokens.access_token;

    afterAll(() => {
        logger.info('Finished ReferenceController tests');
    });

    test('GET /RefOrderStatusesDB should return a list of References', async () => {
        logger.info('Testing GET /RefOrderStatusesDB endpoint');

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/Reference/RefOrderStatusesDB`,
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
            logger.error(`Error testing GET /RefOrderStatusesDB: ${error.message}`);
            done(error);
        });

        req.end();
    });

    test('GET /RefOrderStatusesDB/:id should return a Reference by ID', async () => {
        logger.info('Testing GET /RefOrderStatusesDB/:id endpoint');

        const ReferenceId = 'Cancelled';

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/Reference/RefOrderStatusesDB/${ReferenceId}`,
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
                logger.info(`Received Reference by ID: ${data}`);

                expect(res.statusCode).toBe(200);

                try {
                    const parsedData = JSON.parse(data);
                    expect(parsedData).toHaveProperty('ReferenceId');
                    expect(parsedData.ReferenceId).toBe(ReferenceId);
                } catch (error) {
                    logger.error('Error parsing response data');
                }

                done();
            });
        });

        req.on('error', (error) => {
            logger.error(`Error testing GET /RefOrderStatusesDB/:id: ${error.message}`);
            done(error);
        });

        req.end();
    });

    test('GET /RefShippingMethodsDB should return a list of References', async () => {
        logger.info('Testing GET /RefShippingMethodsDB endpoint');

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/Reference/RefShippingMethodsDB`,
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
            logger.error(`Error testing GET /RefShippingMethodsDB: ${error.message}`);
            done(error);
        });

        req.end();
    });

    test('GET /RefShippingMethodsDB/:id should return a Reference by ID', async () => {
        logger.info('Testing GET /RefShippingMethodsDB/:id endpoint');

        const ReferenceId = 'FedEx';

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/Reference/RefShippingMethodsDB/${ReferenceId}`,
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
                logger.info(`Received Reference by ID: ${data}`);

                expect(res.statusCode).toBe(200);

                try {
                    const parsedData = JSON.parse(data);
                    expect(parsedData).toHaveProperty('ReferenceId');
                    expect(parsedData.ReferenceId).toBe(ReferenceId);
                } catch (error) {
                    logger.error('Error parsing response data');
                }

                done();
            });
        });

        req.on('error', (error) => {
            logger.error(`Error testing GET /RefShippingMethodsDB/:id: ${error.message}`);
            done(error);
        });

        req.end();
    });
});
