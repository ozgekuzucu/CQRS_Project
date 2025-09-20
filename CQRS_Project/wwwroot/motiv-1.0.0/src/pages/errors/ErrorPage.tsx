import { Button, Link, Stack } from '@mui/material';
import Image from 'components/base/Image';
import errorImg from '../../assets/404.svg';

const ErrorPage = () => {
  return (
    <Stack alignItems="center" justifyContent="center" spacing={3} sx={{ height: '100vh' }}>
      <Image src={errorImg} sx={{ width: 1 / 2, height: 1 / 2 }} />
      <Button variant="contained" component={Link} href="/">
        Go To Dashboard
      </Button>
    </Stack>
  );
};

export default ErrorPage;
