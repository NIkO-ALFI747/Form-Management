import { type FC } from 'react'
import { Link } from 'react-router'
import { Container, Row, Col } from 'react-bootstrap/'
import { FaArrowLeft } from 'react-icons/fa'
import CreateUserSection from '../components/CreateUserSection/CreateUserSection.tsx'

const SignUpPage: FC = () => {
  return (
    <Container className="position-relative py-3">
      <Link
        className="position-absolute top-0 start-0 m-3"
        to='/'
      >
        <FaArrowLeft size={20} />
      </Link>
      <Row className="justify-content-center mt-5">
        <Col xl={7} lg={8} md={9} sm={10} xs={11}>
          <CreateUserSection />
        </Col>
      </Row>
    </Container>
  )
}

export default SignUpPage