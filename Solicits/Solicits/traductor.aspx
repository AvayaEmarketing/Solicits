﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="traductor.aspx.cs" Inherits="traductor" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge;chrome=1" />
    <title>My Translations - Avaya</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="developer" content="William Ballesteros Blanco (wballesteros@avaya.com) - Avaya Inc. 2014">

    <!-- Le styles -->
    <link href="css/bootstrap.css" rel="stylesheet">

    <link href="css/bootstrap-responsive.css" rel="stylesheet">
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet">
    <link href="css/docs.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="css/messi.css" rel="stylesheet">
    <link href="css/prettyLoader.css" rel="stylesheet">
    <link href="css/DT_bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-dialog.css" rel="stylesheet" type="text/css" />
    <link href="css/calendar.css" rel="stylesheet" type="text/css" />


    <style>
        .btn-file {
            position: relative;
            overflow: hidden;
        }

            .btn-file input[type=file] {
                position: absolute;
                top: 0;
                right: 0;
                min-width: 150%;
                min-height: 100%;
                font-size: 999px;
                text-align: right;
                filter: alpha(opacity=0);
                opacity: 0;
                background: red;
                cursor: inherit;
                display: block;
            }

        p.description {
            font-size: 0.8em;
            padding: 0 1em 1em;
            margin: 0;
        }

        #message {
            font-size: 0.7em;
            position: absolute;
            top: 1em;
            right: 1em;
            width: 350px;
            display: none;
            padding: 1em;
            background: #ffc;
            border: 1px solid #dda;
        }

        .btn-twitter {
			padding-left: 30px;
			background: rgba(0, 0, 0, 0) url(img/twitterIcon.png) -20px 6px no-repeat;
			background-position: -20px 11px !important;
		}
		.btn-twitter:hover {
			background-position:  -20px -18px !important;
		}
    </style>

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="assets/ico/apple-touch-icon-57-precomposed.png">
    <link rel="shortcut icon" href="assets/ico/favicon.png">
</head>

<body>



    <!-- NAVBAR
    ================================================== -->
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container">


                <a class="brand" href="http://avaya.com">
                    <img class="desktop" src="images/avaya-logo.jpg" alt="Avaya" /><img class="mobile" src="images/avaya-logo-mobile.jpg" alt="Avaya" /></a>

                <button type="button" class="btn btn-navbar visible-phone" data-toggle="collapse" data-target=".nav-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>

                <div class="nav-collapse collapse">
                    <ul class="nav">
                        <li><a href="#" id="my_solicits">My Translations</a></li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" id="my_profile">My Profile&nbsp;&nbsp;<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="#" id="information">Information</a></li>
                                <li><a href="#" id="exit">Exit</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <!--/.nav-collapse -->

            </div>
        </div>
    </div>


    <!-- Marketing messaging and featurettes
    ================================================== -->
    <!-- Wrap the rest of the page in another container to center all the content. -->



    <div class="container" id="dt_my_solicits">
        <div class="row-fluid">


            <div class="span9">

                <hr style="margin-top: 0;">

                <div class="row-fluid">
                    <div class="span12">
                        <h2>My Translations</h2>


                        <table id="datatables" cellpadding="0" cellspacing="0" border="0" style="width: 100%; text-align: center; visibility: hidden" class="table table-striped table-bordered">
                            <thead id="thead">
                                <tr>
                                    <th class="sorting" width="15%">Translation Name</th>
                                    <th class="sorting" width="10%">State</th>
                                    <th class="sorting" width="12%">Original Lang</th>
                                    <th class="sorting" width="12%">Translate Lang</th>
                                    <th class="sorting" width="14%">Register Date</th>
                                    <th class="sorting" width="14%">Desired Date</th>
                                    <th class="sorting" width="14%">Estimated Date</th>
                                    <th class="sorting" width="8%">Priority</th>
                                    <th class="sorting" width="7%">Details</th>


                                </tr>
                            </thead>
                            <tbody id="tbody">
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>
                                        <%--<table style="width:24px">
                            <tr>
                                <td>--%>
                                        <div id="toExcel">
                                            <a href="#" id="btnDescargaExcel">
                                                <img src="images/xls.png" alt="to Excel" /></a>
                                        </div>
                                        <%--</td>
                            </tr>
                        </table>--%>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>


                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /.container -->

    <div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-header">
            <h1>Processing...</h1>
        </div>
        <div class="modal-body">
            <div class="progress progress-striped active">
                <div class="bar progress-bar-danger" style="width: 100%;"></div>
            </div>
        </div>
    </div>

    <div class="container" id="calendario">
        <div class="row-fluid">
            <div class="span9">
                <hr style="margin-top: 0;">
                <div class="row-fluid">
                    <div class="span12">
                        <h2>Calendar</h2><br/>
                        <div class="page-header">

                            <div class="pull-right form-inline">
                                <div class="btn-group">
                                    <button class="btn btn-danger" data-calendar-nav="prev"><< Prev</button>
                                    <button class="btn" data-calendar-nav="today">Today</button>
                                    <button class="btn btn-danger" data-calendar-nav="next">Next >></button>
                                </div>
                                <div class="btn-group">
                                    <button class="btn btn-danger" data-calendar-view="year">Year</button>
                                    <button class="btn btn-danger active" data-calendar-view="month">Month</button>
                                    <button class="btn btn-danger" data-calendar-view="week">Week</button>
                                    <button class="btn btn-danger" data-calendar-view="day">Day</button>
                                </div>
                            </div>
                            <h3></h3>
                        </div>
                        <div class="row">
                            <div class="span9">
                                <div id="calendar"></div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /.container -->


    <!-- Footer
    ================================================== -->
    <footer class="footer">

        <div class="container visible-desktop">
            <div class="row" style="padding: 10px 0;">

                <div class="span4 info">
                    <!--<img style="float: none; padding-bottom: 8px" src="assets/img/footer/mail.png">fasdfasf
        <a href="mailto:conferenceHeadquarters@madisonpg.com">ConferenceHeadquarters@madisonpg.com</a>-->
                </div>
                <div class="span4 info">
                    <a href="https://www.facebook.com/avaya">
                        <img src="images/social/facebook.png"></a>
                    <a href="http://twitter.com/avaya">
                        <img src="images/social/twitter.png"></a>
                    <a href="http://www.linkedin.com/company/1494">
                        <img src="images/social/linkedin.png"></a>
                    <a href="http://www.youtube.com/Avayainteractive">
                        <img src="images/social/youtube.png"></a>
                    <a href="http://www.flickr.com/photos/avaya">
                        <img src="images/social/flickr.png"></a>
                    <a href="http://www.avaya.com/blogs/">
                        <img src="images/social/blog.png"></a>
                </div>
            </div>
        </div>

        <div class="container hidden-desktop">
            <div class="row" style="padding: 20px 0;">
                <div class="span6 info">
                    <table cellpadding="0" cellspacing="0" align="center">
                        <!--<tr>
            <td><img style="float: none; padding-bottom: 8px" src="assets/img/footer/phone.png"></td>
            <td align="left">1-866-535-2542 (US only toll-free)
        <br>+1-212-653-1260 (Outside the US)</td>
          </tr>-->
                    </table>
                </div>
                <div class="span6 info">
                    <!--<img style="float: none; padding-bottom: 8px" src="assets/img/footer/mail.png">
        <a href="mailto:conferenceHeadquarters@madisonpg.com">ConferenceHeadquarters@madisonpg.com</a>-->
                </div>
            </div>
        </div>

        <div class="bs-docs-social social-media">
            <div class="container">

                <div class="row  hidden-desktop">
                    <div class="span12">
                        <ul class="bs-docs-social-buttons">
                            <li><a href="https://www.facebook.com/avaya">
                                <img src="images/social/facebook-mobile.png"></a></li>
                            <li><a href="http://twitter.com/avaya">
                                <img src="images/social/twitter-mobile.png"></a></li>
                            <li><a href="http://www.linkedin.com/company/1494">
                                <img src="images/social/linkedin-mobile.png"></a></li>
                            <li><a href="http://www.youtube.com/Avayainteractive">
                                <img src="images/social/youtube-mobile.png"></a></li>
                            <li><a href="http://www.flickr.com/photos/avaya">
                                <img src="images/social/flickr-mobile.png"></a></li>
                            <li><a href="http://www.avaya.com/blogs/">
                                <img src="images/social/blog-mobile.png"></a></li>
                        </ul>
                    </div>
                </div>


                <div class="row">

                    <div class="span12">
                        <ul class="footer-links">
                            <li><a href="https://thesource.avaya.com/avayaPortal/friendly/termsPage">Terms of Use</a></li>
                            <li class="muted">&middot;</li>
                            <li><a href="http://www.avaya.com/gcm/master-usa/en-us/includedcontent/privacy.htm">Privacy Statement</a></li>
                            <li class="muted">&middot;</li>
                            <li><a href="http://www.avaya.com/gcm/master-usa/en-us/includedcontent/cookiepolicy.htm">Cookies</a></li>
                            <li class="muted">&middot;</li>
                            <li class="muted">&copy; 2009-2014 Avaya Inc.</li>
                        </ul>
                    </div>
                </div>

            </div>
        </div>
    </footer>




    <!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/prettyLoader.js"></script>
    <script type="text/javascript" src="js/ajaxfileupload.js"></script>
    <script type="text/javascript" src="js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="js/DT_bootstrap.js"></script>
    <script type="text/javascript" src="js/traductor.js"></script>
    <script type="text/javascript" src="js/bootstrap-datetimepicker.js"></script>
    <script type="text/javascript" src="js/bootstrap-transition.js"></script>
    <script type="text/javascript" src="js/bootstrap-alert.js"></script>
    <script type="text/javascript" src="js/bootstrap-modal.js"></script>
    <script type="text/javascript" src="js/bootstrap-dropdown.js"></script>
    <script type="text/javascript" src="js/bootstrap-scrollspy.js"></script>
    <script type="text/javascript" src="js/bootstrap-tab.js"></script>
    <script type="text/javascript" src="js/bootstrap-tooltip.js"></script>
    <script type="text/javascript" src="js/bootstrap-popover.js"></script>
    <script type="text/javascript" src="js/bootstrap-button.js"></script>
    <script type="text/javascript" src="js/bootstrap-collapse.js"></script>
    <script type="text/javascript" src="js/bootstrap-carousel.js"></script>
    <script type="text/javascript" src="js/bootstrap-typeahead.js"></script>
    <script type="text/javascript" src="js/bootstrap-affix.js"></script>
    <script type="text/javascript" src="js/run_prettify.js"></script>
    <script type="text/javascript" src="js/bootstrap-dialog.js"></script>

    <script type="text/javascript" src="js/mystyle.js"></script>
    <script type="text/javascript" src="js/holder/holder.js"></script>
    <script type="text/javascript" src="js/respond.src.js"></script>

    <script type="text/javascript" src="js/json2.js"></script>

    <script type="text/javascript" src="js/underscore-min.js"></script>
    <script type="text/javascript" src="js/jstz.min.js"></script>
    <script type="text/javascript" src="js/nl-NL.js"></script>
    <script type="text/javascript" src="js/calendar.js"></script>
    <script type="text/javascript" src="js/app.js"></script>

    <%--<script type="text/javascript">
            var disqus_shortname = 'bootstrapcalendar'; // required: replace example with your forum shortname
            (function () {
                var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
                dsq.src = '//' + disqus_shortname + '.disqus.com/embed.js';
                (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
            })();
    </script>--%>

    <script type='text/javascript' src='js/jquery-migrate-1.2.1.js'></script>

    <!--[if lte IE 7]><script src="assets/js/lte-ie7.js"></script><![endif]-->
</body>
</html>
