import { type FC } from 'react'
import Button from 'react-bootstrap/Button'

interface GetUsersButtonProps {
  onClick: () => void
  isLoading: boolean
}

const GetUsersButton: FC<GetUsersButtonProps> = ({ isLoading, onClick }) => {
  return (
    <div className="d-flex justify-content-center align-items-center mb-1">
      <Button
        variant="primary"
        disabled={isLoading}
        onClick={!isLoading ? onClick : undefined}
      >
        {isLoading ? 'Loading…' : 'Load users'}
      </Button>
    </div>
  )
}

export default GetUsersButton