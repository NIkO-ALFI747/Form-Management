import { type FC } from 'react'
import { Link } from 'react-router'
import { FaArrowLeft } from 'react-icons/fa'

const BackButton: FC = () => {
  return (
    <Link
      className="position-absolute top-0 start-0 m-3"
      to='/'
    >
      <FaArrowLeft size={20} />
    </Link>
  )
}

export default BackButton