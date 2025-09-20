import { Box, Drawer, DrawerProps } from '@mui/material';
import DrawerList from './DrawerList';
import { TransitionEventHandler } from 'react';

interface VerticalNavbarProps extends DrawerProps {
  drawerWidth: number;
  mobileOpen: boolean;
  onTransitionEnd?: TransitionEventHandler<HTMLDivElement> | undefined;
  onHandleDrawerClose: () => void;
}

const VerticalNavbar = ({
  drawerWidth,
  mobileOpen,
  onTransitionEnd,
  onHandleDrawerClose,
}: VerticalNavbarProps) => {
  return (
    <Box
      component="nav"
      sx={{ width: { md: drawerWidth }, flexShrink: { sm: 0 } }}
      aria-label="drawer"
    >
      <Drawer
        variant="temporary"
        open={mobileOpen}
        onTransitionEnd={onTransitionEnd}
        onClose={onHandleDrawerClose}
        ModalProps={{
          keepMounted: true, // Better open performance on mobile.
        }}
        sx={{
          display: { xs: 'block', md: 'none' },
          '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
        }}
      >
        <DrawerList />
      </Drawer>

      <Drawer
        variant="permanent"
        sx={{
          display: { xs: 'none', md: 'block' },
          '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
        }}
        open
      >
        <DrawerList />
      </Drawer>
    </Box>
  );
};

export default VerticalNavbar;
