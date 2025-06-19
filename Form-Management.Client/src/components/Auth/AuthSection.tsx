import { type FC, type PropsWithChildren } from 'react'
import { Card, Container, Row, Col } from 'react-bootstrap/'

interface AuthSectionProps extends PropsWithChildren {
  title: string
}

const AuthSection: FC<AuthSectionProps> = ({ title, children }) => {
  return (
    <Card>
      <Card.Header as="h5" className="text-center">{title}</Card.Header>
      <Card.Body>
        <Container>
          <Row className="justify-content-center m-1">
            <Col xxl={8} xl={9} lg={9} md={11} sm={12} xs={12}>
              {children}
            </Col>
          </Row>
        </Container>
      </Card.Body>
    </Card>
  )
}

export default AuthSection