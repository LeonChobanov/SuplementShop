<template>
  <v-container style="padding: 2rem">
    <v-card>
      <v-card-title>Log in</v-card-title>
      <v-card-text>
        <v-form @submit="onSubmit" @reset="onReset" v-if="show">
          <v-text-field label="Enter email" v-model="form.email"></v-text-field>

          <v-text-field
            label="Enter password"
            type="password"
            v-model="form.password"
          ></v-text-field>

          <v-btn text block to="/sign-up">Dont have an account?</v-btn>
          <br />
          <v-btn type="reset" outlined rounded color="red">Reset</v-btn>
          <v-btn type="submit" outlined rounded color="primary">Submit</v-btn>
        </v-form>
      </v-card-text>
    </v-card>
  </v-container>
</template>


<script>
export default {
  layout: 'auth',
  data() {
    return {
      form: {
        email: "",
        password: "",
      },
      show: true,
    };
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      this.$store.dispatch("userState/logIn", this.form);
    },
    onReset(evt) {
      evt.preventDefault();
      // Reset our form values
      this.form.email = "";
      this.form.password = "";
      // Trick to reset/clear native browser form validation state
      this.show = false;
      this.$nextTick(() => {
        this.show = true;
      });
    },
  },
};
</script>