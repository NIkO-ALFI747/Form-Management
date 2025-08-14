import axios from "../axiosConfig.ts";
import type { SignUpRequest } from "../contracts/SignUpRequest.tsx";
import type { FetchUserRequest } from "../contracts/FetchUserRequest.tsx";
import type { LoginRequest } from "../contracts/LoginRequest.tsx";

export const fetchUsers = async () => {
  try {
    const response = await axios.get<FetchUserRequest[]>(
      import.meta.env.VITE_USERS_URL
    );
    return response.data;
  } catch (e) {
    alert(e);
  }
};

export const signUpUser = async (user: SignUpRequest) => {
  try {
    const response = await axios.post<FetchUserRequest>(
      import.meta.env.VITE_SIGNUP_URL,
      user
    );
    return response.data;
  } catch (e) {
    throw e;
  }
};

export const loginUser = async (user: LoginRequest) => {
  try {
    const response = await axios.post(import.meta.env.VITE_LOGIN_URL, user);
    return response.status;
  } catch (e) {
    throw e;
  }
};

export const signOutUser = async () => {
  try {
    const response = await axios.get(import.meta.env.VITE_SIGNOUT_URL);
    return response.status;
  } catch (e) {
    throw e;
  }
};

export const deleteUsers = async (ids: number[]) => {
  try {
    await axios.delete(import.meta.env.VITE_USERS_URL, {
      data: ids,
    });
  } catch (e) {
    alert(e);
  }
};
