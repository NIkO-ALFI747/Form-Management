import { type FC } from 'react'
import { Container, Row, Col } from 'react-bootstrap/'
import CreateUserSection from './components/CreateUserSection/CreateUserSection.tsx'
import UsersSection from './components/UsersSection/UsersSection.tsx'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'
import './App.css'

const App: FC = () => {
  return (
    <Container>
      <Row className="justify-content-center mt-5">
        <Col xl={7} lg={8} md={9} sm={10} xs={11}>
          <CreateUserSection />
        </Col>
      </Row>
      <Row className="justify-content-center my-4">
        <Col xl={8} lg={9} md={10} sm={11} xs={12}>
          <UsersSection />
        </Col>
      </Row>
    </Container>
  )
}

export default App