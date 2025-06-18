import { type FC } from 'react'
import { Container, Row, Col } from 'react-bootstrap/'
import UsersSection from '../components/UsersSection/UsersSection.tsx'

const UsersManagementPage: FC = () => {
  return (
    <Container>
      <Row className="justify-content-center my-4">
        <Col xl={8} lg={9} md={10} sm={11} xs={12}>
          <UsersSection />
        </Col>
      </Row>
    </Container>
  )
}

export default UsersManagementPage