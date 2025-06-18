import { type FC } from 'react'
import { Link } from 'react-router'
import { Container, Row, Col, Button, Form } from 'react-bootstrap/'
import { FaArrowLeft } from 'react-icons/fa'

interface LoginPageProps {
  setIsAuth: React.Dispatch<React.SetStateAction<boolean>>
}

const LoginPage: FC<LoginPageProps> = ({ setIsAuth }) => {
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
          <Form>
            <Form.Group className="mb-3" controlId="formEmail">
              <Form.Label>Email address</Form.Label>
              <Form.Control type="email" placeholder="Enter email" />
            </Form.Group>
            <Form.Group className="mb-3" controlId="formPassword">
              <Form.Label>Password</Form.Label>
              <Form.Control type="password" placeholder="Password" />
            </Form.Group>
            <Button
              variant="primary"
              type="submit"
              onClick={() => setIsAuth(true)}
            >
              Login
            </Button>
          </Form>
        </Col>
      </Row>
    </Container >
  )
}

export default LoginPage