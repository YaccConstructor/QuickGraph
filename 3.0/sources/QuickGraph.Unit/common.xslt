<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" 
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:mdnet="http://QuickGraph.net/"
	>
	<xsl:param name="separate-fixtures"/>
	<xsl:param name="creation-time" />
	<xsl:param name="show-fixtures-summary" />
	<xsl:template name="meta-html">
		<link rel="stylesheet" href="quickgraph.css" type="text/css" />
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
		<script type="text/javascript" src="report.js">
			<xsl:text disable-output-escaping="yes"> </xsl:text>
		</script>
	</xsl:template>
	<xsl:template name="testbatch-success-literal">
		<xsl:choose>
			<xsl:when test="Counter/@SuccessCount = Counter/@TotalCount">
				<span style="color:green;">SUCCESS</span>
			</xsl:when>
			<xsl:otherwise>
				<span style="color:red;">FAILURE</span>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="duration">
		<xsl:value-of select="format-number(number(@Duration),'0.00')" /> s
	</xsl:template>
	<xsl:template name="duration-split">
		<xsl:variable name="hours" select="floor(number(@Duration) div 3600)" />
		<xsl:variable name="minutes" select="floor((number(@Duration) mod 3600) div 60)" />
		<xsl:variable name="seconds" select="number(@Duration) mod 60" />
		<xsl:value-of select="format-number($hours,'00')" />:
		<xsl:value-of select="format-number($minutes,'00')" />:
		<xsl:value-of select="format-number($seconds,'00.0')" />
	</xsl:template>
	<xsl:template name="tooltip">
		<xsl:attribute name="onMouseOver">showToolTip()</xsl:attribute>
		<xsl:attribute name="onMouseOut">hideToolTip()</xsl:attribute>
	</xsl:template>
	<xsl:template name="mark">
		<xsl:param name="id" />
		<xsl:attribute name="ondoubleclik">javascript:mark('<xsl:value-of select="$id"/>')</xsl:attribute>
	</xsl:template>
	<xsl:template name="toggle">
		<xsl:param name="id" />
		<span class="toggle">
			<xsl:attribute name="id">tg<xsl:value-of select="$id"/></xsl:attribute>
			<xsl:attribute name="onClick">javascript:toggle('tg<xsl:value-of select="$id"/>','<xsl:value-of select="$id"/>')</xsl:attribute>[-]</span>
	</xsl:template>
	<xsl:template name="closed-toggle">
		<xsl:param name="id" />
		<span class="toggle">
			<xsl:attribute name="id">tg<xsl:value-of select="$id"/></xsl:attribute>
			<xsl:attribute name="onClick">javascript:toggle('tg<xsl:value-of select="$id"/>','<xsl:value-of select="$id"/>')</xsl:attribute>[+]</span>
	</xsl:template>
	<xsl:template name="tr-class">
		<xsl:attribute name="class"><xsl:choose>
				<xsl:when test="position() mod 2 = 1">odd</xsl:when>
				<xsl:otherwise>even</xsl:otherwise>
			</xsl:choose></xsl:attribute>
	</xsl:template>
	<xsl:template name="tr-log-class">
		<xsl:attribute name="class"><xsl:choose>
				<xsl:when test="position() mod 2 = 1">log
					<xsl:value-of select="@Level"/>Odd
				</xsl:when>
				<xsl:otherwise>log
					<xsl:value-of select="@Level"/>Even
				</xsl:otherwise>
			</xsl:choose></xsl:attribute>
	</xsl:template>
	<xsl:template name="tr-success-class">
		<xsl:attribute name="class"><xsl:choose>
				<xsl:when test="position() mod 2 = 1">successOdd</xsl:when>
				<xsl:otherwise>successEven</xsl:otherwise>
			</xsl:choose></xsl:attribute>
	</xsl:template>
	<xsl:template name="tr-failure-class">
		<xsl:attribute name="class"><xsl:choose>
				<xsl:when test="position() mod 2 = 1">failureOdd</xsl:when>
				<xsl:otherwise>failureEven</xsl:otherwise>
			</xsl:choose></xsl:attribute>
	</xsl:template>
	<xsl:template name="tr-ignore-class">
		<xsl:attribute name="class"><xsl:choose>
				<xsl:when test="position() mod 2 = 1">ignoreOdd</xsl:when>
				<xsl:otherwise>ignoreEven</xsl:otherwise>
			</xsl:choose></xsl:attribute>
	</xsl:template>
	<xsl:template name="tr-result-class">
		<xsl:choose>
			<xsl:when test="@Failure = 'true'">
				<xsl:call-template name="tr-failure-class"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="tr-success-class"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="fixture-class">
		<xsl:choose>
			<xsl:when test="Counter/@FailureCount > 0">
				<xsl:call-template name="tr-failure-class"/>
			</xsl:when>
			<xsl:when test="Counter/@IgnoreCount > 0">
				<xsl:call-template name="tr-ignore-class"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="tr-success-class"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template name="testbatch-class">
		<xsl:choose>
			<xsl:when test="Counter/@FailureCount > 0">
				<xsl:call-template name="tr-failure-class"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="tr-success-class"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="test-class">
		<xsl:choose>
			<xsl:when test="@State = 'Ignore'">
				<xsl:call-template name="tr-ignore-class"/>
			</xsl:when>
			<xsl:when test="@State = 'Success'">
				<xsl:call-template name="tr-success-class"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="tr-failure-class"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="counter-figures-legend">
		tests, +success, -failures, ~ignored, *new failure, !fixed
	</xsl:template>
	<xsl:template name="counter-figures">
		<xsl:value-of select="Counter/@TotalCount" />,
        <span style="color:green;">+
		<xsl:value-of select="Counter/@SuccessCount" /></span>,
		<span style="color:red;">-
		<xsl:value-of select="Counter/@FailureCount" /></span>,
		<span style="color:blue;">~
		<xsl:value-of select="Counter/@IgnoreCount" />
		<xsl:if test="ancestor-or-self::TestBatch/@HasHistory = 'true'">,
		<span style="color:red">*
		<xsl:value-of select="count(descendant::TestCase[@History='Failure' and @State='Failure'])" />,
		</span>
		<span style="color:green">!
		<xsl:value-of select="count(descendant::TestCase[@History='Fixed'])" />
		</span>
		</xsl:if>
		</span>
	</xsl:template>
	<xsl:template name="counter-percent">
		<xsl:choose>
			<xsl:when test="Counter/@TotalCount - Counter/@IgnoreCount = 0">100.00</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="format-number(Counter/@SuccessCount div number(Counter/@TotalCount - Counter/@IgnoreCount) * 100.0,'0.00')"/>
			</xsl:otherwise>
		</xsl:choose>%
	</xsl:template>
	<xsl:template name="assembly-img">
		<img src="assembly.png" width="16" height="16" border="0"/>
	</xsl:template>
	<xsl:template name="fixture-img">
		<img src="fixture.png" border="0"/>
	</xsl:template>
	<xsl:template name="testcase-img">
		<img src="testcase.png" border="0"/>
	</xsl:template>
	<xsl:template name="log-image">
		<img width="8" height="8" border="0">
			<xsl:attribute name="src">Log<xsl:value-of select="@Level"/>.png</xsl:attribute>
		</img>
	</xsl:template>
	<xsl:template name="assembly-id">
		<xsl:value-of select="@Name"/>
	</xsl:template>
	<xsl:template name="model-id">
		<xsl:value-of select="@Name"/>
	</xsl:template>
	<xsl:template name="submodel-id">
		<xsl:value-of select="@FullName"/>
	</xsl:template>
	<xsl:template name="walk-id">w
		<xsl:value-of select="position()"/>
	</xsl:template>
	<xsl:template name="fixture-id">
		<xsl:value-of select="@id"/>
	</xsl:template>
	<xsl:template name="exception-log">
		<xsl:if test="@ExceptionType">
			<table border="0" cellpadding="1" cellspacing="1" width="100%">
				<xsl:call-template name="exception"/>
			</table>
		</xsl:if>
	</xsl:template>
	<xsl:template name="exception">
		<xsl:variable name="exid" select="generate-id()"/>
		<tr class="failureTitle">
			<td>
				<xsl:call-template name="toggle">
					<xsl:with-param name="id">
						<xsl:value-of select="$exid"/>
					</xsl:with-param>
				</xsl:call-template>
				<xsl:value-of select="@Message" />
			</td>
		</tr>
		<tr class="failureOdd">
			<td>
				<strong>Type:</strong>
				<xsl:value-of select="@ExceptionType" />
			</td>
		</tr>
		<tr class="failureEven">
			<td>
				<strong>Source:</strong>
				<xsl:value-of select="@Source"/>
			</td>
		</tr>
		<xsl:for-each select="Properties/Property">
			<tr>
				<xsl:call-template name="tr-failure-class"/>
				<td>
					<bold>
						<xsl:value-of select="@Name"/>:
					</bold>
					<xsl:value-of select="@Value"/>
				</td>
			</tr>
		</xsl:for-each>
		<tr>
			<td>
				<div class="toggle" style="display:block;">
					<xsl:attribute name="id"><xsl:value-of select="$exid"/></xsl:attribute>
					<pre class="stackTrace">
						<xsl:value-of select="StackTrace/text()" disable-output-escaping="yes"/>
					</pre>
				</div>
			</td>
		</tr>
		<xsl:for-each select="InnerException">
			<xsl:call-template name="exception" />
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="console-output">
		<xsl:if test="ConsoleOut/text()|ConsoleError/text()">
			<table border="0" cellpadding="1" cellspacing="1" width="100%">
				<xsl:for-each select="ConsoleOut">
					<xsl:call-template name="console">
						<xsl:with-param name="name">Console Output</xsl:with-param>
					</xsl:call-template>
				</xsl:for-each>
				<xsl:for-each select="ConsoleError">
					<xsl:call-template name="console">
						<xsl:with-param name="name">Console Output</xsl:with-param>
					</xsl:call-template>
				</xsl:for-each>
			</table>
		</xsl:if>
	</xsl:template>
	<xsl:template name="console">
		<xsl:param name="name" />
		<xsl:variable name="consoleid" select="generate-id()" />
		<xsl:if test="string-length( text() ) != 0">
			<tr class="consoleTitle">
				<td>
					<xsl:call-template name="toggle">
						<xsl:with-param name="id">
							<xsl:value-of select="$consoleid"/>
						</xsl:with-param>
					</xsl:call-template>
					<xsl:value-of select="$name"/>
				</td>
			</tr>
			<tr>
				<td>
					<div class="toggle" style="display:block;">
						<xsl:attribute name="id"><xsl:value-of select="$consoleid"/></xsl:attribute>
						<pre class="console" width="100%">
							<xsl:value-of select="mdnet:FormatConsole(text())" disable-output-escaping="yes"/>
						</pre>
					</div>
				</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<msxsl:script language="C#"  implements-prefix="mdnet">
		<msxsl:using namespace="System.IO" />
		<![CDATA[	
	private static Regex urlRegex = new Regex(@"(?<Protocol>\w+):\/\/\/?(?<Domain>[\w.]+\/?)\S*",
		RegexOptions.IgnoreCase
		| RegexOptions.CultureInvariant
		| RegexOptions.IgnorePatternWhitespace
		| RegexOptions.Compiled
		);
	private static Regex imgRegex = new Regex(@"\[img\s*src=\""(?<Url>.*?)\""\s*/\]",
		RegexOptions.IgnoreCase
		| RegexOptions.CultureInvariant
		| RegexOptions.IgnorePatternWhitespace
		| RegexOptions.Compiled
		);
	private static Regex boldRegex = new Regex(@"\*\*(?<Value>.+?)\*\*",
	    RegexOptions.IgnoreCase
	    | RegexOptions.CultureInvariant
		| RegexOptions.IgnorePatternWhitespace
		| RegexOptions.Compiled
		);

    // XML-escapes a string	
	private string EscapeString(string value)
	{
		using(StringWriter sw = new StringWriter())
		{ 
		    foreach(char c in value)
			{
				if (c >= 0 && c <= 0x1F && c!=9 && c!=10 && c!=13)
				{
				    sw.Write("&#{0};", (int)c);
				}
				else
				{
				    switch(c)
				    {
				    case '<': sw.Write("&lt;"); break;
				    case '>': sw.Write("&gt;"); break;
				    case '&': sw.Write("&amp;"); break;
					default: sw.Write(c); break;
				    }
				}
			}
			return sw.ToString();
		}
	}
	
	// processes console text to 
	// add Url higlighting, image embedding, etc...
	public string FormatConsole(string value)
	{
		string result = EscapeString(value);
		result = urlRegex.Replace(result, "<a href=\"$&\" target=\"_blank\">$&</a>");
		result = imgRegex.Replace(result, "<img src=\"${Url}\" alt=\"${Url}\" />");
		result = boldRegex.Replace(result, "<b>${Value}</b>");
		return result;
	}
]]>		
	</msxsl:script>
	
	<xsl:template name="machine-section">
		<div class="closedToggle" id="unitmachine">
		<h3>Machine details</h3>
			<xsl:for-each select="Machine">
				<xsl:call-template name="machine-description"/>
			</xsl:for-each>
		</div>
	</xsl:template>
	<xsl:template name="machine-description">
		<table border="0" cellpadding="1" cellspacing="1">
			<tr class="heading">
				<td colspan="2">Machine description</td>
			</tr>
			<tr class="even">
				<td>Machine name</td>
				<td>
					<xsl:value-of select="@MachineName"/>
				</td>
			</tr>
			<tr class="odd">
				<td>Framework</td>
				<td>
					<xsl:value-of select="@FrameworkVersion"/>
				</td>
			</tr>
			<tr class="even">
				<td>OS</td>
				<td>
					<xsl:value-of select="@OperatingSystem"/>
				</td>
			</tr>
			<xsl:for-each select="EnvironmentVariables/EnvironmentVariable">
				<tr>
					<xsl:call-template name="tr-class" />
					<td>
						<xsl:value-of select="@Name"/>
					</td>
					<td>
						<xsl:value-of select="@Value"/>
					</td>
				</tr>
			</xsl:for-each>
		</table>
	</xsl:template>
	<xsl:template name="log">
		<xsl:if test="count(Log/LogEntries/LogEntry)>0">
			<xsl:for-each select="Log">
				<table border="0" cellpadding="1" cellspacing="1">
					<tr class="heading">
						<td colspan="3">Log 
							<img src="LogError.png" width="8" height="8" border="0"/>
							<xsl:text> </xsl:text>
							<xsl:value-of select="count(LogEntries/LogEntry[@Level='Error'])"/> errors,
							<xsl:text> </xsl:text>
							<img src="LogWarning.png" width="8" height="8" border="0"/>
							<xsl:text> </xsl:text>
							<xsl:value-of select="count(LogEntries/LogEntry[@Level='Warning'])"/> warnings,
							<xsl:text> </xsl:text>
							<img src="LogMessage.png" width="8" height="8" border="0"/>
							<xsl:text> </xsl:text>
							<xsl:value-of select="count(LogEntries/LogEntry[@Level='Message'])"/> messages
						</td>
					</tr>
					<xsl:for-each select="LogEntries/LogEntry">
						<xsl:call-template name="log-entry" />
					</xsl:for-each>
				</table>
			</xsl:for-each>
		</xsl:if>
	</xsl:template>
	<xsl:template name="log-entry">
		<tr>
			<xsl:call-template name="tr-log-class" />
			<td>
				<xsl:call-template name="log-image" />
			</td>
			<td>
				<xsl:value-of select="@Message"/>
<xsl:if test="Exception">
   <br/>
   <table width="100%">
   <xsl:call-template name="exception" />
   </table>
</xsl:if>
			</td>
		</tr>
	</xsl:template>
	<xsl:template name="escape-filename">
		<xsl:param name="filename" />
		<xsl:value-of select="translate(translate(translate(translate($filename,';','_'),')','_'),'.','_'),'(','_')"/>
	</xsl:template>
	<xsl:template name="fixture-detail-link">
		<xsl:call-template name="escape-filename">
			<xsl:with-param name="filename" select="/TestBatch/@EntryAssemblyName"/>
		</xsl:call-template>_
		<xsl:call-template name="escape-filename">
			<xsl:with-param name="filename"  select="@Name"/>
		</xsl:call-template>.html
	</xsl:template>
	<xsl:template name="fixture-titlerow">
		<tr>
			<xsl:call-template name="fixture-class" />
			<td>
				<xsl:choose>
					<xsl:when test="$separate-fixtures">
						<a>
							<xsl:attribute name="href"><xsl:call-template name="fixture-detail-link"/></xsl:attribute>
							<xsl:attribute name="target">_blank</xsl:attribute>
						</a>
					</xsl:when>
					<xsl:otherwise>
						<a>
							<xsl:attribute name="href">#<xsl:call-template name="fixture-id"/></xsl:attribute>
						</a>
					</xsl:otherwise>
				</xsl:choose>
				<xsl:call-template name="fixture-img" />
				<xsl:text> </xsl:text>
				<xsl:value-of select="@Name"/>
			</td>
			<td>
				<xsl:call-template name="counter-percent"/>
			</td>
			<td>
				<xsl:call-template name="counter-figures"/>
			</td>
			<td>
				<xsl:call-template name="duration"/>
			</td>
		</tr>
	</xsl:template>
	<xsl:template name="fixtures-summary">
		<xsl:if test="$show-fixtures-summary">
		<h3>Fixtures summary</h3>
		<table border="0" cellpadding="1" cellspacing="1" width="100%">
			<xsl:for-each select="//Fixtures/Fixture">
				<xsl:sort select="Counter/@FailureCount" order="descending" data-type="number"/>
				<xsl:sort select="Counter/@IgnoreCount" order="descending" data-type="number"/>
				<xsl:call-template name="fixture-titlerow"/>
			</xsl:for-each>
		</table>
		</xsl:if>
	</xsl:template>
	<xsl:template name="new-test-failures">
		<xsl:variable name="testcount" select="count(descendant::TestCase[@History='Failure'])" />
		<xsl:if test="$testcount > 0">
		<h3>
			<xsl:call-template name="toggle">
				<xsl:with-param name="id">newfailures</xsl:with-param>
			</xsl:call-template> New failures (<xsl:value-of select="$testcount" />)
		</h3>
		<div class="toggle" id="newfailures">
			<table border="0" cellpadding="1" cellspacing="1" width="100%">
				<xsl:for-each select="//TestCase[@History='Failure']">
					<tr>
						<xsl:call-template name="test-class"/>
						<td>
							<xsl:call-template name="test-title-link"/>
						</td>
					</tr>
				</xsl:for-each>
			</table>
	<xsl:text disable-output-escaping="yes"> </xsl:text>
		</div>
		</xsl:if>
	</xsl:template>

	<xsl:template name="fixed-tests">
		<xsl:variable name="testcount" select="count(//TestCase[@History='Fixed'])" />
		<xsl:if test="$testcount > 0">
		<h3>
			<xsl:call-template name="toggle">
				<xsl:with-param name="id">fixedtests</xsl:with-param>
			</xsl:call-template> Fixed tests (<xsl:value-of select="$testcount" />)
		</h3>
		<div class="toggle" id="fixedtests">
			<table border="0" cellpadding="1" cellspacing="1" width="100%">
				<xsl:for-each select="//TestCase[@History='Fixed']">
					<tr>
						<xsl:call-template name="test-class"/>
						<td>
							<xsl:call-template name="test-title-link"/>
						</td>
					</tr>
				</xsl:for-each>
			</table>
	<xsl:text disable-output-escaping="yes"> </xsl:text>
		</div>
		</xsl:if>
	</xsl:template>
	<xsl:template name="new-tests">
		<xsl:variable name="testcount" select="count(//TestCase[@History='New'])" />
		<xsl:if test="$testcount > 0">
		<h3>
			<xsl:call-template name="toggle">
				<xsl:with-param name="id">newtests</xsl:with-param>
			</xsl:call-template> New tests (<xsl:value-of select="$testcount" />)
		</h3>
		<div class="toggle" id="newtests">
			<table border="0" cellpadding="1" cellspacing="1" width="100%">
				<xsl:for-each select="//TestCase[@History='New']">
					<tr>
						<xsl:call-template name="test-class"/>
						<td>
							<xsl:call-template name="test-title-link"/>
						</td>
					</tr>
				</xsl:for-each>
			</table>
	<xsl:text disable-output-escaping="yes"> </xsl:text>
		</div>
		</xsl:if>
	</xsl:template>


	<xsl:key name="exception-by-type" match="Exception" use="@ExceptionType" />
	<xsl:template name="tests-by-exceptions">
		<xsl:if test="//Exception">
			<div class="closedToggle" id="unitexceptions">
			<h3>Throwed exceptions</h3>
				<table border="0" cellpadding="1" cellspacing="1" width="100%">
					<xsl:for-each select="//Exception[count(. | key('exception-by-type', @ExceptionType)[1]) = 1]">
						<xsl:sort select="@ExceptionType" />
						<xsl:variable name="etype" select="@ExceptionType" />
						<xsl:variable name="eid" select="generate-id()" />
						<tr>
							<xsl:call-template name="tr-class"/>
							<td>
								<xsl:call-template name="closed-toggle">
									<xsl:with-param name="id" select="$eid"/>
								</xsl:call-template>
								<xsl:value-of select="@ExceptionType" />,
								<xsl:value-of select="count(//TestCase[descendant::Exception[@ExceptionType= $etype]])"/> exceptions
								<div class="closedToggle">
									<xsl:attribute name="id"><xsl:value-of select="$eid"/></xsl:attribute>
									<table border="0" cellpadding="1" cellspacing="1" width="100%">
										<xsl:for-each select="//TestCase[descendant::Exception[@ExceptionType= $etype]]">
											<tr>
												<xsl:call-template name="test-class"/>
												<td>
													<xsl:call-template name="test-title-link"/>
												</td>
											</tr>
										</xsl:for-each>
									</table>
								</div>
							</td>
						</tr>
					</xsl:for-each>
				</table>
	<xsl:text disable-output-escaping="yes"> </xsl:text>
			</div>
		</xsl:if>
	</xsl:template>
	<xsl:template name="fixtures-details">
		<xsl:for-each select="Fixtures/Fixture">
			<xsl:sort select="Counter/@FailureCount" order="descending" data-type="number"/>
			<xsl:sort select="Counter/@IgnoreCount" order="descending" data-type="number"/>
			<xsl:call-template name="fixture-detail" />
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="fixture-detail">
		<xsl:variable name="reproid">rf<xsl:call-template name="fixture-id"/></xsl:variable>
		<xsl:variable name="divid">div<xsl:call-template name="fixture-id"/></xsl:variable>
		<h3>
			<xsl:attribute name="id">
				<xsl:call-template name="fixture-id"/>
			</xsl:attribute>
			<xsl:call-template name="closed-toggle">
				<xsl:with-param name="id" select="$divid" />
			</xsl:call-template>
			<xsl:value-of select="@Name"/>,
			<xsl:call-template name="counter-figures" />
			<xsl:call-template name="copy-repro">
				<xsl:with-param name="id" select="$reproid" />
			</xsl:call-template>
		</h3>
		<div class="closedToggle">
			<xsl:attribute name="id"><xsl:value-of select="$divid"/></xsl:attribute>
		<table class="indented"  border="0" cellpadding="1" cellspacing="1" width="100%">
			<tr class="repro">
				<td>
					<xsl:attribute name="id">
						<xsl:value-of select="$reproid"/>
					</xsl:attribute>
					<xsl:call-template name="fixture-repro"/>
				</td>
			</tr>
			<tr class="even">
				<td>
					<xsl:if test="count(@Description)>0">
						<xsl:value-of select="@Description"/>,
					</xsl:if>
					Categories=
					<xsl:value-of select="@Categories"/>
					,ApartementState.
					<xsl:value-of select="@Apartment"/>
					,TimeOut=
					<xsl:value-of select="@TimeOut"/> [min]
				</td>
			</tr>
		</table>
		</div>
		<table class="indented"  border="0" cellpadding="1" cellspacing="1" width="100%">
			<xsl:for-each select="FixtureSetUp">
				<xsl:call-template name="test-result"/>
			</xsl:for-each>
			<xsl:for-each select="FixtureTearDown">
				<xsl:call-template name="test-result"/>
			</xsl:for-each>
		</table>
		<xsl:call-template name="tests-details" />
	</xsl:template>
	<xsl:template name="tests-details">
		<table border="0" cellpadding="1" cellspacing="1" width="100%">
			<xsl:for-each select="TestCases/TestCase">
				<xsl:sort select="@State"/>
				<xsl:call-template name="test-detail"/>
			</xsl:for-each>
		</table>
	</xsl:template>
	<xsl:template name="copy-repro">
		<xsl:param name="id" />
		<span class="button">
			<xsl:attribute name="onClick">javascript:copyToClipboad('<xsl:value-of select="$id"/>')</xsl:attribute>
			Repro
		</span>		
	</xsl:template>
	<xsl:template name="fixture-repro">
			<xsl:value-of select="//TestBatch/@EntryAssemblyLocation"/> 
			/ff:"<xsl:value-of select="@Name"/>" 
			/or+
	</xsl:template>
	<xsl:template name="test-repro">
		<xsl:value-of select="//TestBatch/@EntryAssemblyLocation"/> 
		/ff:"<xsl:value-of select="ancestor::Fixture/@Name"/>" 
		/tcf:"<xsl:value-of select="@Name"/>" /or+ /orh-
	</xsl:template>
	<xsl:template name="longuest-tests">
		<div class="closedToggle" id="unitlongesttests">
		<h3>
            10 longuest tests
		</h3>
			<table border="0" cellpadding="1" cellspacing="1" width="100%">
				<xsl:for-each select="//TestCase">
					<xsl:sort data-type="number" order="descending" select="@Duration"/>
					<xsl:if test="position() &lt; 10">
						<tr>
							<xsl:call-template name="test-class"/>
							<td>
								<xsl:call-template name="test-title-link"/>
							</td>
							<td>
								<xsl:call-template name="duration" />
							</td>
						</tr>
					</xsl:if>
				</xsl:for-each>
			</table>
		</div>
	</xsl:template>
	<xsl:template name="longuest-fixtures">
		<div class="closedToggle" id="unitlongestfixtures">
		<h3>
            10 longuest fixtures
		</h3>
			<table border="0" cellpadding="1" cellspacing="1" width="100%">
				<xsl:for-each select="//Fixture">
					<xsl:sort data-type="number" order="descending" select="@Duration"/>
					<xsl:if test="position() &lt; 10">
						<xsl:call-template name="fixture-titlerow"/>
					</xsl:if>
				</xsl:for-each>
			</table>
		</div>
	</xsl:template>
	<xsl:template name="test-toggle">
		<xsl:variable name="testid" select="@id" />
				<xsl:call-template name="closed-toggle">
					<xsl:with-param name="id">
						<xsl:value-of select="$testid"/>
					</xsl:with-param>
				</xsl:call-template>
	</xsl:template>
	<xsl:template name="test-title-link">
		<a>
			<xsl:choose>
				<xsl:when test="not($separate-fixtures)">
					<xsl:attribute name="href">#<xsl:value-of select="@id"/></xsl:attribute>
				</xsl:when>
				<xsl:otherwise>
					<xsl:attribute name="href"><xsl:for-each select ="ancestor::Fixture">
							<xsl:call-template name="fixture-detail-link"/>
						</xsl:for-each>#<xsl:value-of select="@id"/></xsl:attribute>
					<xsl:attribute name="target">_blank</xsl:attribute>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:call-template name="test-title"/>
		</a>
	</xsl:template>
	<xsl:template name="test-title">
		<xsl:call-template name="testcase-img"/>
		<xsl:value-of select="@Name"/>
		<xsl:choose>
		<xsl:when test="@History='Failure'">
			<span class="newfailure">New Failure!</span>
		</xsl:when>
		<xsl:when test="@History='Fixed'">
			<span class="fixed">Fixed!</span>
		</xsl:when>
            <xsl:when test="@History='New'">
                <span class="newtest">New!</span>
            </xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="test-detail">
		<xsl:variable name="testid" select="@id" />
		<xsl:variable name="rowid">td<xsl:value-of select="@id"/></xsl:variable>
		<xsl:variable name="reproid">rt<xsl:value-of select="@id"/></xsl:variable>
		<tr>
			<xsl:call-template name="test-class"/>
			<td>
				<xsl:attribute name="id"><xsl:value-of select="$rowid"/></xsl:attribute>
				<xsl:call-template name="mark">
					<xsl:with-param name="id" select="$rowid" />
				</xsl:call-template>
				<xsl:call-template name="test-toggle"/>
				<xsl:call-template name="test-title" />,
				<xsl:call-template name="duration" />
				<xsl:call-template name="copy-repro">
					<xsl:with-param name="id" select="$reproid" />
				</xsl:call-template>
				<br/>
				<div>
					<xsl:attribute name="class">closedToggle</xsl:attribute>
					<xsl:attribute name="id"><xsl:value-of select="$testid"/></xsl:attribute>
					<table class="indented" border="0" cellpadding="1" cellspacing="1"  width="100%">
						<tr class="repro">
							<td>
								<xsl:attribute name="id"><xsl:value-of select="$reproid"/></xsl:attribute>
								<xsl:call-template name="test-repro" />
							</td>
						</tr>
						<xsl:for-each select="SetUp">
							<xsl:call-template name="test-result"/>
						</xsl:for-each>
						<xsl:for-each select="Test">
							<xsl:call-template name="test-result"/>
						</xsl:for-each>
						<xsl:for-each select="TearDown">
							<xsl:call-template name="test-result"/>
						</xsl:for-each>
					</table>
				</div>
			</td>
		</tr>
	</xsl:template>
	<xsl:template name="test-result">
		<xsl:variable name="testbodyid" select="generate-id()" />
		<xsl:variable name="hasbody" select="count(Exception/@ExceptionType)>0 or count(Log/LogEntries/LogEntry) > 0 or count(ConsoleOut/text())>0 or count(ConsoleError/text())>0" />
		<tr>
			<xsl:call-template name="test-class"/>
			<td>
				<xsl:if test="$hasbody">
					    <xsl:call-template name="toggle">
						    <xsl:with-param name="id">
							    <xsl:value-of select="$testbodyid" />
						    </xsl:with-param>
					    </xsl:call-template>
				</xsl:if>
				<xsl:value-of select="name()"/>
				<xsl:if test="$hasbody">
					<br/>
					<div class="toggle">
						<xsl:attribute name="id"><xsl:value-of select="$testbodyid"/></xsl:attribute>
						<xsl:for-each select="Exception">
							<xsl:call-template name="exception-log"/>
						</xsl:for-each>
						<xsl:call-template name="log"/>
						<xsl:call-template name="console-output"/>
					</div>
				</xsl:if>
			</td>
		</tr>
	</xsl:template>
	<xsl:template name="unit-copyright">
		<hr />
		<p class="copyright">Report generated by QuickGraph.Unit, QuickGraph.NET Suite.</p>
		<p class="copyright">
      <img src="quickgraph.png"/>
      <img src="operations.png"/>
			<img src="unit.png"/>
		</p>
	</xsl:template>
</xsl:stylesheet>