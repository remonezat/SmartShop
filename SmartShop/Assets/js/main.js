$(".showNav").on('click',function(){
  // $(".side-nav__head__icon").hide()
  // $(".side-nav__head__icon-left").show()
  // $(".col-2 .hideNav").hide()
  // $(".col-2 .showNav").show()
  
  $(".col-2 p").fadeToggle(500)
  $(".col-2 .arrow").fadeToggle(500) 
  $(".col-2 .rightblue").fadeToggle(500)
  $(".col-2 .leftblue").fadeToggle(500)


  $(".col-2 ").toggleClass('foo')
    
 
});






// $(" .showNav").on('click',function(){
//   $(".col-2 p").show(500)
//   $(".col-2 .arrow").show(500) 
//   $(".col-2 .rightblue").show(500)
//   $(".col-2 .leftblue").show(500)
//   $(".col-2 ").css({"width":"230px",'transition':'0.5s' })

// });




var loadFile = function(event) {
    $('.imgoutput').css('background','#FFF')
    var image = document.getElementById('output');
    image.src = URL.createObjectURL(event.target.files[0]);
  };
  


  $('.showTable').click(function(){
    $('.hiddenTable').fadeToggle()
    $('.showTable .up').fadeToggle(500)
    $('.showTable .down').fadeToggle(400)

  })

  $('#bills .icons, #bills h3').click(function(){
    $('.bills').fadeToggle()
    $('#bills .up').fadeToggle(500)
    $('#bills .down').fadeToggle(400)

  })

  $('#bills input').click(function(){
    $('.bills input:checkbox').not(this).prop('checked', this.checked);
   
  })

 

  $('#expensess .icons, #expensess h3').click(function(){
    $('.expensess').fadeToggle()
    $('#expensess .up').fadeToggle(500)
    $('#expensess .down').fadeToggle(400)

  })

  $('#expensess input').click(function(){
    $('.expensess input:checkbox').not(this).prop('checked', this.checked);
   
  })

  $('#employee .icons, #employee h3').click(function(){
    $('.employee').fadeToggle()
    $('#employee .up').fadeToggle(500)
    $('#employee .down').fadeToggle(400)

  })

  $('#employee input').click(function(){
    $('.employee input:checkbox').not(this).prop('checked', this.checked);
   
  })


  $('#accounts .icons, #accounts h3').click(function(){
    $('.accounts').fadeToggle()
    $('#accounts .up').fadeToggle(500)
    $('#accounts .down').fadeToggle(400)

  })

  $('#accounts input ').click(function(){
    $('.accounts input:checkbox').not(this).prop('checked', this.checked);
   
  })



  $('#items .icons, #items h3').click(function(){
    $('.items').fadeToggle()
    $('#items .up').fadeToggle(500)
    $('#items .down').fadeToggle(400)

  })


  $('#items input').click(function(){
    $('.items input:checkbox').not(this).prop('checked', this.checked);
   
  })


  $('#transaction .icons, #transaction h3').click(function(){
    $('.transaction').fadeToggle()
    $('#transaction .up').fadeToggle(500)
    $('#transaction .down').fadeToggle(400)

  })

  $('#transaction input').click(function(){
    $('.transaction input:checkbox').not(this).prop('checked', this.checked);
   
  })



  $('#reports .icons, #reports h3').click(function(){
    $('.reports').fadeToggle()
    $('#reports .up').fadeToggle(500)
    $('#reports .down').fadeToggle(400)

  })

  $('#reports input').click(function(){
    $('.reports input:checkbox').not(this).prop('checked', this.checked);
   
  })


  $('.popup .add').click(function(){
    $('.sale').fadeToggle()
   
  })

  $('.showPopup').click(function(){
    $('.popup').fadeIn()
    $('.cover').fadeIn()

  })



  $('body').click(()=>{
  
  })