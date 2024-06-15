import axios from "axios";

export function signUp(body) {
  return axios.post('https://localhost:7164/api/User/register', body, { // Use the full backend URL
    headers: {
      "Content-Type": "application/json", // Ensure content-type header is included
      "Accept-Language": "tr"
    }
  });
}
