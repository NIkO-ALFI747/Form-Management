import axios from "axios"
import type { IUser } from "../types/User.tsx"
import type { CreateUserRequest } from "../contracts/CreateUserRequest.tsx"

export const fetchUsers = async () => {
  try {
    const response = await axios.get<IUser[]>(import.meta.env.VITE_USERS_URL)
    return response.data
  } catch (e) {
    alert(e)
  }
}

export const createUser = async (user: CreateUserRequest) => {
  try {
    const response = await axios.post<IUser>(import.meta.env.VITE_USERS_URL, user)
    return response.data
  } catch (e) {
    alert(e)
  }
}