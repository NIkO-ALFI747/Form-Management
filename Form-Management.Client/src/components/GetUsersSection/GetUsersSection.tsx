import { useState, useEffect, type FC } from 'react'
import Card from 'react-bootstrap/Card'
import UsersTable from "./UsersTable/UsersTable.tsx"
import GetUsersButton from './GetUsersButton.tsx'
import type { IUser } from "../../types/User.tsx"
import { fetchUsers } from '../../services/users.ts'

const GetUsersSection: FC = () => {
  const [users, setUsers] = useState<IUser[]>([])
  const [isLoading, setLoading] = useState<boolean>(false)

  const onFetch = async () => {
    setLoading(true)
    const users = await fetchUsers()
    setUsers(users!)
    setLoading(false)
  }

  const onClick = () => onFetch()

  useEffect(() => {
    onFetch()
  }, [])

  return (
    <Card>
      <Card.Header as="h5" className="text-center">Users</Card.Header>
      <Card.Body>
        <GetUsersButton isLoading={isLoading} onClick={onClick} />
        <UsersTable users={users} />
      </Card.Body>
    </Card>
  )
}

export default GetUsersSection