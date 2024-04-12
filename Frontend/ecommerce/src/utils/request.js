import axios from "axios";

const httpRequest = axios.create({
  baseURL: "https://localhost:7242/api/v1",
  headers: { Accept: "application/json", "Content-Type": "application/json" },
});

export default httpRequest;