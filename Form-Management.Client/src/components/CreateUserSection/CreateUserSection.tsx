import { type FC } from 'react'
import { Card, Container, Row, Col } from 'react-bootstrap/'
import Form from './Form/Form.tsx'

const CreateUserSection: FC = () => {
  return (
    <Card>
      <Card.Header as="h5" className="text-center">Create User</Card.Header>
      <Card.Body>
        <Container>
          <Row className="justify-content-center m-1">
            <Col xxl={8} xl={9} lg={9} md={11} sm={12} xs={12}>
              <Form />
            </Col>
          </Row>
        </Container>
      </Card.Body>
    </Card>
  )
}

export default CreateUserSection