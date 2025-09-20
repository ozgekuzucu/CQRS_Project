import { PropsWithChildren, useState } from 'react';
import { Box, Toolbar, Stack } from '@mui/material';
import VerticalNavbar from './drawer/VerticalNavbar';
import TopBar from './topbar/TopBar';
import Footer from './footer/Footer';

const drawerWidth = 248;

const MainLayout = ({ children }: PropsWithChildren) => {
  const [mobileOpen, setMobileOpen] = useState(false);
  const [isClosing, setIsClosing] = useState(false);

  const handleDrawerClose = () => {
    setIsClosing(true);
    setMobileOpen(false);
  };

  const handleDrawerTransitionEnd = () => {
    setIsClosing(false);
  };

  const handleDrawerToggle = () => {
    if (!isClosing) {
      setMobileOpen(!mobileOpen);
    }
  };

  return (
    <Stack direction="row">
      <TopBar drawerWidth={drawerWidth} onHandleDrawerToggle={handleDrawerToggle} />
      <VerticalNavbar
        drawerWidth={drawerWidth}
        mobileOpen={mobileOpen}
        onTransitionEnd={handleDrawerTransitionEnd}
        onHandleDrawerClose={handleDrawerClose}
      />
      <Box
        component="main"
        sx={(theme) => ({
          flexGrow: 1,
          p: { xs: theme.spacing(3.75, 3), md: theme.spacing(3.75, 5.375, 3.75, 3.75) },
          minHeight: '100vh',
          width: { xs: 1, sm: `calc(100% - ${drawerWidth}px)` },
          bgcolor: 'grey.100',
        })}
      >
        <Toolbar />
        {children}
        <Footer />
      </Box>
    </Stack>
  );
};

export default MainLayout;
