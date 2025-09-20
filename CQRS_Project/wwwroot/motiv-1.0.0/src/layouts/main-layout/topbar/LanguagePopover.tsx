import MenuItem from '@mui/material/MenuItem';
import IconButton from '@mui/material/IconButton';
import { MouseEvent, useState } from 'react';
import IconifyIcon from 'components/base/IconifyIcon';
import { ListItemIcon, Menu, Typography } from '@mui/material';
import { LanguageItem } from 'types/types';

const languageItems: LanguageItem[] = [
  {
    id: 0,
    value: 'ban',
    label: 'Bengali',
    icon: 'twemoji:flag-bangladesh',
  },
  {
    id: 1,
    value: 'en',
    label: 'English',
    icon: 'twemoji:flag-england',
  },
  {
    id: 2,
    value: 'fr',
    label: 'French',
    icon: 'twemoji:flag-france',
  },
];

const LanguagePopover = () => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedIndex, setSelectedIndex] = useState(1);
  const open = Boolean(anchorEl);

  const handleClickItem = (event: MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuItemClick = (id: number) => {
    setSelectedIndex(id);
    setAnchorEl(null);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <>
      <IconButton
        onClick={handleClickItem}
        color="inherit"
        aria-label="language"
        aria-controls={open ? 'language-menu' : undefined}
        aria-haspopup="true"
        aria-expanded={open ? 'true' : undefined}
      >
        <IconifyIcon icon={languageItems[selectedIndex].icon} />
      </IconButton>

      <Menu
        id="language-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
      >
        {languageItems.map((languageItem) => (
          <MenuItem
            key={languageItem.id}
            selected={languageItem.id === selectedIndex}
            onClick={() => handleMenuItemClick(languageItem.id)}
          >
            <ListItemIcon>
              <IconifyIcon icon={languageItem.icon} />
            </ListItemIcon>
            <Typography variant="body2">{languageItem.label}</Typography>
          </MenuItem>
        ))}
      </Menu>
    </>
  );
};

export default LanguagePopover;
