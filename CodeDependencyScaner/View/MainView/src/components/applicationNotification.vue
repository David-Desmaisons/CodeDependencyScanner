<template>
  <div class="wrapper application-notification">
    <notifications group="app" :reverse="true" position="top center" :width="700">
      <template slot="body" scope="props">
        <div class="ui icon message" :class="props.item.type">
          <i :class="[icon(props.item.type), 'icon']"></i>
          <i class="close icon" @click="props.close"></i>
          <div class="content">
            <div class="header">
              {{props.item.title}}
            </div>
            <p>{{props.item.text}}</p>
          </div>
        </div>
      </template>
    </notifications>
  </div>
</template>

<script>
const icons = {
  error: 'warning sign',
  warn: 'warning',
  information: 'info',
  success: 'checkmark'
}

export default {
  methods: {
    showMessage(message) {
      const notification = {
        title: message.Title,
        type: message.Type,
        text: message.Content,
        duration: message.duration || (message.Type === 'error') ? -1 : 4000,
        group: 'app'
      }
      this.$notify(notification);
      this.$emit('notified', notification)
    },
    icon(type) {
      return icons[type] || icons.information
    }
  }
}

</script>

<style>
.application-notification.wrapper>.notifications {
  margin-top: 40px;
}

.application-notification .ui.message {
  opacity: 0.95;
  background: #555555;
  box-shadow: 0px 1px 2px 0px rgba(0, 0, 0, 0.2);
  font-size: 1rem;
  text-align: center;
  color: rgba(0, 0, 0, 0.87);
  border-radius: 0.28571429rem;
  -webkit-transition: 0.2s background ease;
  transition: 0.2s background ease;
}

.application-notification .ui.icon.message>.icon:not(.close){
  width: 40px;
  font-size: 2em;
}

.application-notification .ui.icon.message{
  color: white;
}

.application-notification .ui.icon.message .header:not(.ui){
	font-size: 1.28571429rem;
  color: white;
}

.application-notification .ui.message>.title {
  color: #FFFFFF;
}

.application-notification .ui.message>.close.icon {
  opacity: 0.4;
  color: #FFFFFF;
  -webkit-transition: opacity 0.2s ease;
  transition: opacity 0.2s ease;
}



/*******************************
             States
*******************************/


/* Hover */

.application-notification .ui.message:hover {
  background: #555555;
  opacity: 1;
}

.application-notification .ui.message .close:hover {
  opacity: 1;
}


/*--------------
     Variations
---------------*/


.application-notification .ui.message.error{
  background: red;
  opacity: 0.95;
}

.application-notification .ui.message.warn{
  background: orange;
  opacity: 0.95;
}

.application-notification .ui.message.success{
  background: green;
  opacity: 0.95;
}

.application-notification .ui.message.information{
  background: blue;
  opacity: 0.95;
}

.application-notification .ui.message.error:hover{
  background: red;
  opacity: 1;
}

.application-notification .ui.message.warn:hover{
  background: orange;
  opacity: 1;
}

.application-notification .ui.message.success:hover{
  background: green;
  opacity: 1;
}

.application-notification .ui.message.information:hover{
  background: blue;
  opacity: 1;
}

</style>