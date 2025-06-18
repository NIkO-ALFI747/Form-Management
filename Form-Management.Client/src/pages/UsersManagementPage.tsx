import { type FC } from 'react'
import { Container, Row, Col } from 'react-bootstrap/'
import UsersSection from '../components/UsersSection/UsersSection.tsx'
import NavBar from '../components/NavBar/NavBar.tsx'

interface UsersManagementPageProps {
  isAuth: boolean
  setIsAuth: React.Dispatch<React.SetStateAction<boolean>>
}

const UsersManagementPage: FC<UsersManagementPageProps> = ({ isAuth, setIsAuth }) => {
  return (
    <>
      <NavBar isAuth={isAuth} setIsAuth={setIsAuth} />
      <Container>
        <Row className="justify-content-center my-4">
          <Col xl={8} lg={9} md={10} sm={11} xs={12}>
            <UsersSection />
          </Col>
        </Row>
      </Container>
    </>
  )
}

export default UsersManagementPage