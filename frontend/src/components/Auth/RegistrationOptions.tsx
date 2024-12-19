import React from 'react';
import { Box, Typography, Button, Grid, Card, CardContent } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const RegistrationOptions: React.FC = () => {
  const navigate = useNavigate();

  const handleRedirect = (path: string) => {
    navigate(path);
  };

  return (
    <Box display="flex" justifyContent="center" alignItems="center" minHeight="100vh">
      <Grid container spacing={3} justifyContent="center">
        {/* Register as Influencer */}
        <Grid item xs={12} sm={6} md={4}>
          <Card>
            <CardContent>
              <Typography variant="h5" gutterBottom>
                Register as Influencer
              </Typography>
              <Typography variant="body2" color="textSecondary" paragraph>
                Join our platform as an influencer to collaborate with brands and grow your audience.
              </Typography>
              <Button
                variant="contained"
                color="primary"
                onClick={() => handleRedirect('/register-influencer')}
                fullWidth
              >
                Register as Influencer
              </Button>
            </CardContent>
          </Card>
        </Grid>

        {/* Register as Company */}
        <Grid item xs={12} sm={6} md={4}>
          <Card>
            <CardContent>
              <Typography variant="h5" gutterBottom>
                Register as Company
              </Typography>
              <Typography variant="body2" color="textSecondary" paragraph>
                Join our platform as a company to connect with influencers and promote your brand.
              </Typography>
              <Button
                variant="contained"
                color="primary"
                onClick={() => handleRedirect('/register-company')}
                fullWidth
              >
                Register as Company
              </Button>
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </Box>
  );
};

export default RegistrationOptions;
