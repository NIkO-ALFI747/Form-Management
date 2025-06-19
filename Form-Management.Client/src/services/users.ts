import axios from "axios"
import type { SignUpRequest } from "../contracts/SignUpRequest.tsx"
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

export const signUpUser = async (user: SignUpRequest) => {
  try {
    const response = await axios.post<FetchUserRequest>(
      import.meta.env.VITE_USERS_URL, user
    )
    return response.data
  } catch (e) {
    throw e
  }
}

export const deleteUsers = async (ids: number[]) => {
  try {
    await axios.delete(import.meta.env.VITE_USERS_URL, { data: ids })
  } catch (e) {
    alert(e)
  }
}
