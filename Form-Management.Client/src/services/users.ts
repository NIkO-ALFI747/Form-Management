import axios from "axios"
import type { CreateUserRequest } from "../contracts/CreateUserRequest.tsx"
import type { FetchUserRequest } from "../contracts/FetchUserRequest.tsx"

export const fetchUsers = async () => {
  try {
    const response = await axios.get<FetchUserRequest[]>(
      import.meta.env.VITE_USERS_URL
    )
    return response.data
  } catch (e) {
    alert(e)
  }
}

export const createUser = async (user: CreateUserRequest) => {
  try {
    const response = await axios.post<FetchUserRequest>(
      import.meta.env.VITE_USERS_URL, user
    )
    return response.data
  } catch (e) {
    alert(e)
  }
}