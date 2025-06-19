import { type FC } from 'react'
import { Container, Row, Col } from 'react-bootstrap/'
import BackButton from '../components/Auth/BackButton.tsx'
import AuthSection from '../components/Auth/AuthSection.tsx'
import SignUpForm from '../components/Auth/SignUpForm/SignUpForm.tsx'

interface SignUpPageProps {
  setIsAuth: React.Dispatch<React.SetStateAction<boolean>>
}

const SignUpPage: FC<SignUpPageProps> = ({ setIsAuth }) => {
  return (
    <Container className="position-relative py-3">
      <BackButton />
      <Row className="justify-content-center mt-5">
        <Col xl={7} lg={8} md={9} sm={10} xs={11}>
          <AuthSection title='Sign Up'><SignUpForm setIsAuth={setIsAuth} /></AuthSection>
        </Col>
      </Row>
    </Container>
  )
}

export default SignUpPage