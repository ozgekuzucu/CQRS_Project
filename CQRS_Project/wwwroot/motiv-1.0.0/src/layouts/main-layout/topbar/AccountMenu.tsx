import { Avatar, IconButton, ListItemIcon, Menu, MenuItem, Typography } from '@mui/material';
import avatar from 'assets/avatar.webp';
import IconifyIcon from 'components/base/IconifyIcon';
import { MouseEvent, useState } from 'react';
import { MenuItem as SingleMenuItem } from 'types/types';

const menuItems: SingleMenuItem[] = [
  {
    id: 0,
    label: 'Profile',
    icon: 'material-symbols:person',
  },
  {
    id: 1,
    label: 'My Account',
    icon: 'material-symbols:account-box-sharp',
  },
  {
    id: 2,
    label: 'Logout',
    icon: 'uiw:logout',
  },
];

const AccountMenu = () => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const handleClick = (event: MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <>
      <IconButton
        onClick={handleClick}
        color="inherit"
        aria-label="account"
        aria-controls={open ? 'account-menu' : undefined}
        aria-haspopup="true"
        aria-expanded={open ? 'true' : undefined}
      >
        <Avatar sx={{ width: 40, height: 40 }} alt="avatar" src={avatar} />
      </IconButton>

      <Menu
        id="account-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        onClick={handleClose}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
      >
        {menuItems.map((menuItem) => (
          <MenuItem key={menuItem.id} onClick={handleClose}>
            <ListItemIcon>
              <IconifyIcon icon={menuItem.icon} />
            </ListItemIcon>
            <Typography variant="body2">{menuItem.label}</Typography>
          </MenuItem>
        ))}
      </Menu>
    </>
  );
};

export default AccountMenu;
