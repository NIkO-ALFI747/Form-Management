import { type FC } from 'react'
import Table from 'react-bootstrap/Table'
import UserRow from './UserRow.tsx'
import type { IUser } from '../../../types/User.tsx'
import classes from './UsersTable.module.css'

interface UsersTableProps {
  users: IUser[]
}

const UsersTable: FC<UsersTableProps> = ({ users }) => {
  return (
    <div className={`${classes.tableBox} overflow-auto m-auto`}>
      <Table hover responsive className="text-nowrap">
        <thead>
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>Email</th>
            <th>Password</th>
          </tr>
        </thead>
        <tbody>
          {
            users.map((user) => <UserRow user={user} key={user.id} />)
          }
        </tbody>
      </Table>
    </div>
  )
}

export default UsersTable