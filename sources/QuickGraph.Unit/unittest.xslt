<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:include href ="common.xslt"/>
    <xsl:template match="/TestBatch">
		<html>
			<xsl:comment> saved from url=(0027)http://blog.dotnetwiki.org/ </xsl:comment>
			<head>
                <title>
                    <xsl:value-of select="@EntryAssemblyLocation"/> Test Report
                </title>
                <xsl:call-template name="meta-html" />
            </head>
            <body>
                <h1>
                <img src="unit.png" border="0" /><xsl:text> </xsl:text>
                     <xsl:value-of select="@EntryAssemblyLocation"/> Test Report:
                    <xsl:call-template name="testbatch-success-literal" />, <br/>
				</h1>
               <p>
                  <xsl:call-template name="counter-figures" />
                  <span style="font-size:smaller">
                  (<xsl:call-template name="counter-figures-legend"/>)
                  </span>
               </p>
	               <xsl:call-template name="test-batch" />
                <xsl:call-template name="unit-copyright" />
            </body>
        </html>
    </xsl:template>

	<xsl:template name="literal-counters">
		<xsl:variable name="newfailurecount" select="count(descendant::TestCase[@History='New'])" />
		<xsl:variable name="fixedcount" select="count(descendant::TestCase[@History='Fixed'])" />
		New failures: <xsl:value-of select="$newfailurecount"/>,
        Fixed tests: <xsl:value-of select="$fixedcount"/>,
        Start Time: <xsl:value-of select="@StartTime"/>,
		End Time: <xsl:value-of select="@EndTime"/>,
		Duration: <xsl:call-template name="duration-split"/>
	</xsl:template>
	
    <xsl:template name="test-batch">
        <xsl:call-template name="menu" />
		<xsl:call-template name="batch-log" />
        <xsl:call-template name="longuest-tests" />
        <xsl:call-template name="longuest-fixtures"/>
        <xsl:call-template name="tests-by-exceptions" />
		<xsl:call-template name="machine-section"/>
        <xsl:call-template name="new-test-failures"/>
        <xsl:call-template name="fixed-tests"/>
        <xsl:call-template name="new-tests"/>
		
		<xsl:call-template name="fixtures-summary"/>
        <xsl:for-each select="TestAssemblies/TestAssembly">
            <xsl:call-template name="test-assembly" />
        </xsl:for-each>
    </xsl:template>

    <xsl:template name="menu">
<p class="menu">
<xsl:call-template name="menu-item">
   <xsl:with-param name="id">unitlog</xsl:with-param>
   <xsl:with-param name="text">Log</xsl:with-param>
</xsl:call-template>|
<xsl:call-template name="menu-item">
   <xsl:with-param name="id">unitlongesttests</xsl:with-param>
   <xsl:with-param name="text">Longuest tests</xsl:with-param>
</xsl:call-template>|
<xsl:call-template name="menu-item">
   <xsl:with-param name="id">unitlongestfixtures</xsl:with-param>
   <xsl:with-param name="text">Longuest fixtures</xsl:with-param>
</xsl:call-template>|
<xsl:if test="//Exception">
<xsl:call-template name="menu-item">
   <xsl:with-param name="id">unitexceptions</xsl:with-param>
   <xsl:with-param name="text">Exceptions</xsl:with-param>
</xsl:call-template>|
</xsl:if>
<xsl:call-template name="menu-item">
   <xsl:with-param name="id">unitmachine</xsl:with-param>
   <xsl:with-param name="text">Machine</xsl:with-param>
</xsl:call-template>
</p>
    </xsl:template>

    <xsl:template name="menu-item">
      <xsl:param name="id"/>
      <xsl:param name="text"/>
<xsl:call-template name="closed-toggle">
<xsl:with-param name="id" select="$id" />
</xsl:call-template>
<xsl:value-of select="$text" />
    </xsl:template>

    <xsl:template name="batch-log">
    <xsl:variable name="id">batchLog</xsl:variable>
<div class="closedToggle" id="unitlog">
<h3>Log</h3>
<xsl:call-template name="log" />
	<xsl:text disable-output-escaping="yes"> </xsl:text>
</div>
    </xsl:template>

    <xsl:template name="assembly-setupandteardown">
        <xsl:if test="count(AssemblySetUp) >0 or count(AssemblyTearDown) > 0">
            <h3>Assembly setup and teardown</h3>
            <table  border="0" cellpadding="1" cellspacing="1"  width="100%">
                <xsl:for-each select="AssemblySetUp">
                    <xsl:call-template name="test-result"/>
                </xsl:for-each>
                <xsl:for-each select="AssemblyTearDown">
                    <xsl:call-template name="test-result"/>
                </xsl:for-each>
            </table>
        </xsl:if>
    </xsl:template>
    
    <xsl:template name="test-assembly">
		<xsl:variable name="aid">a<xsl:value-of select="generate-id()"/></xsl:variable>
        <h2>
			<xsl:call-template name="closed-toggle">
				<xsl:with-param name="id" select="$aid" />
			</xsl:call-template>
            <xsl:value-of select="@AssemblyName"/>, <xsl:call-template name="counter-figures"/>
        </h2>
		<div class="closedToggle">
			<xsl:attribute name="id"><xsl:value-of select="$aid"/></xsl:attribute>
        <table border="0" cellpadding="1" cellspacing="1" width="100%">
            <tr class="even">
                <td>FullName</td>
                <td>
                    <xsl:value-of select="@AssemblyFullName"/>
                </td>
            </tr>
            <tr class="odd">
                <td>Location</td>
                <td>
                    <xsl:value-of select="@AssemblyLocation"/>
                </td>
            </tr>
            <tr class="even">
                <td>StartTime</td>
                <td>
                    <xsl:value-of select="@StartTime"/>
                </td>
            </tr>
            <tr class="odd">
                <td>StartTime</td>
                <td>
                    <xsl:value-of select="@EndTime"/>
                </td>
            </tr>
        </table>
		</div>
		<xsl:call-template name="assembly-setupandteardown" />
        <xsl:if test="not($separate-fixtures)">
            <xsl:call-template name="fixtures-details" />
        </xsl:if>
    </xsl:template>
</xsl:stylesheet>