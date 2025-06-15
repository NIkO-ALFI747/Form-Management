import type { CreateUserRequest } from "../../../../contracts/CreateUserRequest"

export interface InputGroupProps {
  user: CreateUserRequest | null
  setUser: React.Dispatch<React.SetStateAction<CreateUserRequest | null>>
}