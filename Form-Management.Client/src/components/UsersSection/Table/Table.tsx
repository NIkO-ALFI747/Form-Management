import { type FC } from 'react'
import { Table as BtsTable } from 'react-bootstrap/'
import type { User } from '../types/User.tsx'
import classes from './Table.module.css'
import THead from './THead/THead.tsx'
import TBody from './TBody/TBody.tsx'

interface TableProps {
  users: User[]
  setUsers: React.Dispatch<React.SetStateAction<User[]>>
  selectedCount: number
  setSelectedCount: React.Dispatch<React.SetStateAction<number>>
}

const Table: FC<TableProps> = ({ users, setUsers, selectedCount, setSelectedCount }) => {
  return (
    <div className={`${classes.tableBox} overflow-auto m-auto`}>
      <BtsTable hover responsive className="text-nowrap">
        <THead users={users} setUsers={setUsers} selectedCount={selectedCount} setSelectedCount={setSelectedCount}/>
        <TBody users={users} setUsers={setUsers} setSelectedCount={setSelectedCount}/>
      </BtsTable>
    </div>
  )
}

export default Table