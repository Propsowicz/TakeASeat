 //const url = 'https://localhost:7252';

 const url = 'http://localhost:5000';

 const typHeader = {
     'Content-Type': 'application/json',
     Authorization: localStorage.getItem('JWTokens') ? `Bearer ${JSON.parse(localStorage.getItem('JWTokens')).accessToken}` : null,
 };

 export { url, typHeader };
