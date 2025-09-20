import { Box, CircularProgress, Stack, StackOwnProps } from '@mui/material';

const Progress = (props: StackOwnProps) => {
  return (
    <Stack justifyContent="center" alignItems="center" height="100vh" {...props}>
      <Box height={50} width={50}>
        <CircularProgress />
      </Box>
    </Stack>
  );
};

export default Progress;
