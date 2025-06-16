import { useState, type FC, useEffect } from 'react'
import Card from 'react-bootstrap/Card'
import Table from "./Table/Table.tsx"
import type { User } from "./types/User.tsx"
import Toolbar from './Toolbar/Toolbar.tsx'
import { fetchUsers as fetchUsersService } from '../../services/users.ts'
import type { FetchUserRequest } from '../../contracts/FetchUserRequest.tsx'

const UsersSection: FC = () => {

  const [users, setUsers] = useState<User[]>([])

  const [selectedCount, setSelectedCount] = useState<number>(0)

  const [isLoading, setLoading] = useState<boolean>(false)

  const parseFetchedUsersToUsers = (fetchedUsers: FetchUserRequest[]) => {
    return fetchedUsers.map(fetchedU => {
      return {
        ...fetchedU, isSelected:
          users.find(u => fetchedU.id === u.id)?.isSelected ?? false
      }
    })
  }

  const fetchUsers = async () => {
    const fetchedUsers = await fetchUsersService()
    const users = parseFetchedUsersToUsers(fetchedUsers!)
    setUsers(users)
    setSelectedCount(users.reduce((acc, u) => acc + (u.isSelected ? 1 : 0), 0))
  }

  const loadUsers = async () => {
    setLoading(true)
    await fetchUsers()
    setLoading(false)
  }

  useEffect(() => {
    loadUsers()
  }, [])

  return (
    <Card>
      <Card.Header as="h5" className="text-center">Users</Card.Header>
      <Card.Body>
        <Toolbar isLoading={isLoading} setUsers={setUsers} selectedCount={selectedCount}
          setSelectedCount={setSelectedCount} loadUsers={loadUsers}
        />
        <Table users={users} setUsers={setUsers} selectedCount={selectedCount} setSelectedCount={setSelectedCount} />
      </Card.Body>
    </Card>
  )
}

export default UsersSection