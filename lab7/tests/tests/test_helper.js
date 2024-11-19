'use strict';

const axios = require('axios');
const qs = require('qs');

const DOMAIN = 'halaiko-vitalii.eu.auth0.com';
const CLIENT_ID = 'zJfZc5Gxuo0ySyQhHzBrdEgziXbBMrrU';
const CLIENT_SECRET = 'iaNp5vz0iieC7eU-5Hby8DAj0kC2swikq0HxhoEdlNabwcxQA_2U5jOein9SpE34';
const AUDIENCE = `https://${DOMAIN}/api/v2/`;
const LOGIN = 'vitalikhall5@gmail.com';
const PASSWORD = 'Qwer1234asdf1234_';

const getUserTokens = async () => {
    const options = {
        method: 'POST',
        url: `https://${DOMAIN}/oauth/token`,
        headers: { 'content-type': 'application/x-www-form-urlencoded' },
        data: qs.stringify({
            grant_type: 'password',
            username: LOGIN,
            password: PASSWORD,
            audience: AUDIENCE,
            scope: 'openid profile offline_access',
            client_id: CLIENT_ID,
            client_secret: CLIENT_SECRET
        })
    };

    try {
        const response = await axios(options);
        return response.data;
    } catch (error) {
        throw new Error('Invalid credentials or request failed');
    }
};

module.exports = { getUserTokens };
