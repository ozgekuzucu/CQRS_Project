import { Box, LinearProgress, Stack, StackOwnProps } from '@mui/material';

const LinearLoader = (props: StackOwnProps) => {
  return (
    <Stack justifyContent="center" alignItems="center" height="100vh" {...props}>
      <Box width={'50vw'}>
        <LinearProgress />
      </Box>
    </Stack>
  );
};

export default LinearLoader;
