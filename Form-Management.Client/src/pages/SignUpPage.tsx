import { type FC } from 'react'
import { Container, Row, Col } from 'react-bootstrap/'
import CreateUserSection from '../components/CreateUserSection/CreateUserSection.tsx'

const SignUpPage: FC = () => {
  return (
    <Container>
      <Row className="justify-content-center mt-5">
        <Col xl={7} lg={8} md={9} sm={10} xs={11}>
          <CreateUserSection />
        </Col>
      </Row>
    </Container>
  )
}

export default SignUpPage