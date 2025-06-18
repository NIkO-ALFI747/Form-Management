import { type FC } from 'react'
import NavBar from '../components/NavBar/NavBar.tsx'

interface HomePageProps {
  isAuth: boolean
  setIsAuth: React.Dispatch<React.SetStateAction<boolean>>
}

const HomePage: FC<HomePageProps> = ({ isAuth, setIsAuth }) => {
  return (
    <>
      <NavBar isAuth={isAuth} setIsAuth={setIsAuth} />
      <h1 className="text-center mt-5">Home</h1>
    </>
  )
}

export default HomePage